using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace EmuCPU.Editing {
	
	/// <summary>
	/// Менеджер помощи
	/// </summary>
	public class HelpManager : Panel {

		/// <summary>
		/// Корневая страница
		/// </summary>
		HelpPage BasePage;

		/// <summary>
		/// Основная страница
		/// </summary>
		HelpPage NotFoundPage;

		/// <summary>
		/// Текущая страница
		/// </summary>
		HelpPage current;

		/// <summary>
		/// Контролы
		/// </summary>
		Control[] controls;

		/// <summary>
		/// Флаг перестройки контролов
		/// </summary>
		bool rebuilding;

		/// <summary>
		/// Конструктор менеджера помощи
		/// </summary>
		public HelpManager() {

			// Поля контрола
			DoubleBuffered = true;
			AutoScroll = false;
			HorizontalScroll.Enabled = false;
			HorizontalScroll.Visible = false;
			HorizontalScroll.Maximum = 0;
			AutoScroll = true;
			
			// Страницы
			NotFoundPage = new HelpPage("notfound", "Не найдено", new HelpElement[]{
				new TextElement("Не найдено", new Font("MicrosoftSansSerif", 14, FontStyle.Bold), Color.FromArgb(100, 0, 0)),
				new TextElement("Внезапно, но запрошенная страница отсутствует в справке. Попробуйте вернутсья на главную и повторите поиск по страницам."),
				new LinkBlockElement(new LinkBlockElement.Link[] {
					new LinkBlockElement.Link("На главную", "/")
				})
			});

			// Разбор файла на страницы
			if (!DesignMode) {
				XmlDocument helpDoc = new XmlDocument();
				try {
					helpDoc.Load("help.xml");
					foreach (XmlNode el in helpDoc.ChildNodes) {
						if (el.Name.ToLower() == "page") {
							BasePage = RecursiveParse(el, "");
							break;
						}
					}
				} catch (Exception) {}
			}
			
			GetPage("/");
		}

		/// <summary>
		/// Рекурсивный парсинг нодов
		/// </summary>
		/// <param name="nd">Корневая страница</param>
		/// <param name="parent">Родительская страница</param>
		/// <returns>Страница помощи</returns>
		HelpPage RecursiveParse(XmlNode nd, string parent) {
			if (nd.Name.ToLower() != "page") {
				throw new Exception("Ошибка разбора справки");
			}

			// Списки для вложенных данных
			List<HelpElement> elems = new List<HelpElement>();
			List<HelpPage> childs = new List<HelpPage>();
			
			// Поля страницы
			string title = GetAttr(nd, "Title");
			string link = GetAttr(nd, "Address");

			// Проход по дочерним элементам
			foreach (XmlNode el in nd.ChildNodes) {
				switch (el.Name.ToLower()) {

					case "page":
						// Страница
						childs.Add(RecursiveParse(el, link));
						break;

					case "text":
					case "p":
						// Текст
						string fname = GetAttr(el, "Font", "MicrosoftSansSerif");
						string fsize = GetAttr(el, "Size", "8,25");
						string fcolor = GetAttr(el, "Color", "Black");
						string ftype = GetAttr(el, "Style", "");
						string ftxt = el.InnerText.Replace('\n', ' ').Replace('\r', ' ');

						// Разбор данных
						float fcalc = 8.25f;
						float.TryParse(fsize, out fcalc);
						Color clr = Color.FromName(fcolor);
						FontStyle fs = FontStyle.Regular;
						switch (ftype.ToLower()) {
							case "bold":
								fs = FontStyle.Bold;
								break;

							case "italic":
								fs = FontStyle.Italic;
								break;
						}
						elems.Add(new TextElement(ftxt, new Font(fname, fcalc, fs), clr));
						break;

					case "links":
						// Блок ссылок
						List<LinkBlockElement.Link> links = new List<LinkBlockElement.Link>();
						foreach (XmlElement le in el.ChildNodes) {
							if (le.Name.ToLower() == "a") {
								string to = GetAttr(le, "To", "/");
								string txt = le.InnerText;
								links.Add(new LinkBlockElement.Link(txt, to));
							}
						}
						elems.Add(new LinkBlockElement(links.ToArray()));
						break;

					default:
						break;
				}
			}

			// Отдача страницы
			return new HelpPage(link, title, elems.ToArray(), childs.ToArray());
		}



		/// <summary>
		/// Получение справки по указанной ссылке
		/// </summary>
		/// <param name="link">Ссылка</param>
		/// <returns>Содержимое веб-документа</returns>
		public void GetPage(string link) {
			if (link.ToLower().StartsWith("http")) {
				System.Diagnostics.Process.Start(link);
				return;
			}
			HelpPage pp = FindPage(BasePage, link);
			if (pp != null) {
				RebuildLayout(pp);
			} else {
				RebuildLayout(NotFoundPage);
			}
		}

		/// <summary>
		/// Поиск страницы
		/// </summary>
		/// <param name="p">Страница для проверки</param>
		/// <param name="lnk">Ссылка</param>
		/// <returns>Найденную страницу или null</returns>
		HelpPage FindPage(HelpPage p, string lnk, string sub="/") {
			
			if (p == null) {
				return null;
			}
			if (p.Children != null) {
				foreach (HelpPage hp in p.Children) {
					HelpPage hg = FindPage(hp, lnk, p == BasePage ? "/" : sub + p.Address + "/");
					if (hg != null) {
						return hg;
					}
				}
			}
			if (lnk == sub+p.Address) {
				return p;
			}
			return null;
		}

		/// <summary>
		/// Перестройка страницы помощи
		/// </summary>
		void RebuildLayout(HelpPage pg) {
			current = pg;
			rebuilding = true;
			SuspendLayout();
			VerticalScroll.Value = 0;
			Controls.Clear();
			if (pg != null) {
				controls = pg.Render();
				Controls.AddRange(controls);
			} else {
				controls = null;
			}
			rebuilding = false;
			Rearrange();
			ResumeLayout();
		}

		/// <summary>
		/// Перерасстановка гаджетов
		/// </summary>
		void Rearrange() {
			if (current != null && controls != null) {
				int px = 0, py = 0;
				int w = ClientSize.Width;
				Graphics g = Graphics.FromImage(new Bitmap(1, 1));
				int[] hs = current.MeasureHeight(w, g);
				for (int i = 0; i < controls.Length; i++) {
					controls[i].Bounds = new Rectangle(
						px, py,
						w, hs[i]
					);
					py += hs[i] + 3;
				}
			}
		}

		/// <summary>
		/// Перестройка контролов
		/// </summary>
		protected override void OnResize(EventArgs eventargs) {
			if (rebuilding) {
				return;
			}
			SuspendLayout();
			VerticalScroll.Value = 0;
			Rearrange();
			ResumeLayout();
		}

		/// <summary>
		/// Получение атрибута узла
		/// </summary>
		/// <param name="elem">Элемент</param>
		/// <param name="name">Имя</param>
		/// <param name="def">Стандартное значение</param>
		/// <returns></returns>
		string GetAttr(XmlNode elem, string name, string def = "") {
			if (elem.Attributes!=null) {
				XmlAttribute attr = elem.Attributes[name];
				if (attr != null) {
					return attr.Value;
				}
			}
			return def;
		}

		/// <summary>
		/// Страница помощи
		/// </summary>
		class HelpPage {

			/// <summary>
			/// Адрес страницы
			/// </summary>
			public string Address {
				get;
				private set;
			}

			/// <summary>
			/// Заголовок страницы
			/// </summary>
			public string Title {
				get;
				private set;
			}

			/// <summary>
			/// Дочерние элементы
			/// </summary>
			public List<HelpPage> Children {
				get;
				private set;
			}

			/// <summary>
			/// Родительская страница
			/// </summary>
			public HelpPage Parent {
				get;
				private set;
			}

			/// <summary>
			/// Элементы помощи
			/// </summary>
			public HelpElement[] Elements {
				get;
				private set;
			}

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="link">Ссылка на страницу</param>
			/// <param name="title">Заголовок</param>
			/// <param name="elems">Элементы</param>
			public HelpPage(string link, string title, HelpElement[] elems, HelpPage[] childs = null) {
				Address = link;
				Title = title;
				Elements = elems;
				Children = new List<HelpPage>();
				if (childs != null) {
					Children.AddRange(childs);
					foreach (HelpPage pg in childs) {
						pg.Parent = this;
					}
				}
			}

			/// <summary>
			/// Превращение в контролы
			/// </summary>
			/// <returns>Массив элементов управления</returns>
			public Control[] Render() {
				List<Control> cl = new List<Control>();
				if (Elements != null) {
					foreach (HelpElement h in Elements) {
						cl.AddRange(h.Render());
					}
				}
				return cl.ToArray();
			}

			/// <summary>
			/// Вычисление высоты контрола
			/// </summary>
			/// <returns>Ширина в пикселях</returns>
			public int[] MeasureHeight(int width, Graphics g) {
				List<int> cl = new List<int>();
				if (Elements != null) {
					foreach (HelpElement h in Elements) {
						cl.AddRange(h.MeasureHeight(width, g));
					}
				}
				return cl.ToArray();
			}
		}

		/// <summary>
		/// Один абстрактный элемент
		/// </summary>
		abstract class HelpElement {

			/// <summary>
			/// Вывод контрола
			/// </summary>
			public abstract Control[] Render();

			/// <summary>
			/// Вычисление высоты контрола
			/// </summary>
			/// <returns>Ширина в пикселях</returns>
			public abstract int[] MeasureHeight(int width, Graphics g);

		}

		/// <summary>
		/// Текстовый элемент
		/// </summary>
		class TextElement : HelpElement {

			/// <summary>
			/// Цвет текста
			/// </summary>
			public Color TextColor = Color.Black;

			/// <summary>
			/// Шрифт
			/// </summary>
			public Font TextFont = new Font("MicrosoftSansSerif", 8.25f);

			/// <summary>
			/// Текст
			/// </summary>
			public string Text = "";

			/// <summary>
			/// Конструктор с текстом
			/// </summary>
			/// <param name="txt">Текст</param>
			public TextElement(string txt) {
				Text = txt;
			}

			/// <summary>
			/// Конструктор с текстом и параметрами
			/// </summary>
			/// <param name="txt">Текст</param>
			/// <param name="fnt">Шрифт</param>
			/// <param name="clr">Цвет</param>
			public TextElement(string txt, Font fnt, Color clr) {
				Text = txt;
				TextFont = fnt;
				TextColor = clr;
			}

			/// <summary>
			/// Отрисовка
			/// </summary>
			public override Control[] Render() {
				Label l = new Label();
				l.ForeColor = TextColor;
				l.Font = TextFont;
				l.Padding = new Padding(0);
				l.Text = Text.Replace("\t", "    ").Replace("[n]", "\n");
				return new Control[]{ l };
			}

			/// <summary>
			/// Вычисление высоты контрола
			/// </summary>
			/// <param name="width">Ширина</param>
			/// <param name="g">Объект класса Graphics</param>
			/// <returns>Высота в пикселях</returns>
			public override int[] MeasureHeight(int width, Graphics g) {
				return new int[] { (int)g.MeasureString(Text.Replace("\t", "    ").Replace("[n]", "\n"), TextFont, width).Height + 5 };
			}
		}

		/// <summary>
		/// Блок ссылок
		/// </summary>
		class LinkBlockElement : HelpElement {

			/// <summary>
			/// Шрифт
			/// </summary>
			static Font mainFont = new Font("MicrosoftSansSerif", 8.25f);

			/// <summary>
			/// Список ссылок
			/// </summary>
			public Link[] Links {
				get;
				private set;
			}

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="ls">Массив ссылок</param>
			public LinkBlockElement(Link[] ls) {
				Links = ls;
			}

			/// <summary>
			/// Рендер блока ссылок
			/// </summary>
			/// <returns>Контрол</returns>
			public override Control[] Render() {
				List<Control> csr = new List<Control>();
				if (Links!=null) {
					foreach (Link l in Links) {
						LinkLabel ll = new LinkLabel();
						ll.Text = l.Text;
						ll.Tag = (object)l.Address;
						ll.Font = mainFont;
						ll.Padding = Padding.Empty;
						ll.LinkClicked += ll_LinkClicked;
						ll.LinkBehavior = LinkBehavior.HoverUnderline;
						csr.Add(ll);
					}
				}
				return csr.ToArray();
			}

			/// <summary>
			/// Вычисление высоты контрола
			/// </summary>
			/// <param name="width">Ширина</param>
			/// <param name="g">Объект класса Graphics</param>
			/// <returns>Высота в пикселях</returns>
			public override int[] MeasureHeight(int width, Graphics g) {
				int[] pg = new int[Links.Length];
				for (int i = 0; i < pg.Length; i++) {
					pg[i] = (int)g.MeasureString(Links[i].Text.Replace("\t", "    "), mainFont, width).Height + 2;
				}
				return pg;
			}

			/// <summary>
			/// Коллбек для перехода
			/// </summary>
			void ll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
				LinkLabel ll = sender as LinkLabel;
				string t = ll.Tag as string;
				Control c = ll.Parent;
				while (c != null) {
					if (c is HelpManager) {
						(c as HelpManager).GetPage(t);
						break;
					}
					c = c.Parent;
				}
			}

			/// <summary>
			/// Ссылка
			/// </summary>
			public class Link {

				/// <summary>
				/// Текст
				/// </summary>
				public string Text {
					get;
					private set;
				}

				/// <summary>
				/// Адрес
				/// </summary>
				public string Address {
					get;
					private set;
				}

				/// <summary>
				/// Конструктор ссылки
				/// </summary>
				/// <param name="text">Текст</param>
				/// <param name="address">Адрес</param>
				public Link(string text, string address) {
					Text = text;
					Address = address;
				}
			}

		}
	}
}
