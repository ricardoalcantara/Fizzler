using System;

namespace Fizzler
{
	/// <summary>
    /// An <see cref="ISelectorGenerator"/> implementation that generates
    /// human-readable description of the selector.
	/// </summary>
	public class HumanReadableSelectorGenerator : ISelectorGenerator
	{
		private int _chainCount;
        private string _text;

		/// <summary>
		/// Delimits the initialization of a generation.
		/// </summary>
		public virtual void OnInit()
		{
			Text = null;
		}

		/// <summary>
		/// Gets the selector implementation.
		/// </summary>
		/// <remarks>
		/// If the generation is not complete, this property returns the 
		/// last generated selector.
		/// </remarks>
		public string Text
		{
			get { return _text; }
			private set { _text = value; }
		}

		/// <summary>
		/// Delimits a selector generation in a group of selectors.
		/// </summary>
		public virtual void OnSelector()
		{
			if (string.IsNullOrEmpty(Text))
				Text = "Select all nodes";
			else
				Text += ", then combined with previous, select all nodes";
		}

		/// <summary>
		/// Delimits the closing/conclusion of a generation.
		/// </summary>
		public virtual void OnClose()
		{
			Text = Text.Trim();
			Text += ".";
		}

		/// <summary>
		/// Adds a generated selector.
		/// </summary>
		protected void Add(string selector)
		{
			if (selector == null) throw new ArgumentNullException("selector");
			Text += selector;
		}

		/// <summary>
		/// Generates a <a href="http://www.w3.org/TR/css3-selectors/#type-selectors">type selector</a>,
		/// which represents an instance of the element type in the document tree. 
		/// </summary>
		public void Type(string type)
		{
			Add(string.Format(" with the <{0}> tag", type));
		}

		/// <summary>
		/// Generates a <a href="http://www.w3.org/TR/css3-selectors/#universal-selector">universal selector</a>,
		/// any single element in the document tree in any namespace 
		/// (including those without a namespace) if no default namespace 
		/// has been specified for selectors. 
		/// </summary>
		public void Universal()
		{
			Add("");
		}

		/// <summary>
		/// Generates a <a href="http://www.w3.org/TR/css3-selectors/#Id-selectors">ID selector</a>,
		/// which represents an element instance that has an identifier that 
		/// matches the identifier in the ID selector.
		/// </summary>
		public void Id(string id)
		{
			Add(string.Format(" with an id of '{0}'", id));
		}

		void ISelectorGenerator.Class(string clazz)
		{
			Add(" with class name " + clazz);
		}

		/// <summary>
		/// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
		/// that represents an element with the given attribute <paramref name="name"/>
		/// whatever the values of the attribute.
		/// </summary>
		public void AttributeExists(string name)
		{
			Add(string.Format(" which have a {0} attribute", name));
		}

		/// <summary>
		/// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
		/// that represents an element with the given attribute <paramref name="name"/>
		/// and whose value is exactly <paramref name="value"/>.
		/// </summary>
		public void AttributeExact(string name, string value)
		{
			Add(string.Format(" which have a {0} attribute with a value of '{1}'", name, value));
		}

		/// <summary>
		/// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
		/// that represents an element with the given attribute <paramref name="name"/>
		/// and whose value is a whitespace-separated list of words, one of 
		/// which is exactly <paramref name="value"/>.
		/// </summary>
		public void AttributeIncludes(string name, string value)
		{
			Add(string.Format(" which have a {0} attribute which includes the value '{1}'", name, value));
		}

		/// <summary>
		/// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
		/// that represents an element with the given attribute <paramref name="name"/>,
		/// its value either being exactly <paramref name="value"/> or beginning 
		/// with <paramref name="value"/> immediately followed by "-" (U+002D).
		/// </summary>
		public void AttributeDashMatch(string name, string value)
		{
			Add(" which have a {0} attribute with a hyphen separated value matching '{1}'");
		}

        /// <summary>
        /// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
        /// that represents an element with the attribute <paramref name="name"/> 
        /// whose value begins with the prefix <paramref name="value"/>.
        /// </summary>
        public void AttributePrefixMatch(string name, string value)
	    {
            Add(string.Format(" which have a {0} attribute whose value begins with '{1}'", name, value));
        }

	    /// <summary>
	    /// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
	    /// that represents an element with the attribute <paramref name="name"/> 
	    /// whose value ends with the prefix <paramref name="value"/>.
	    /// </summary>
	    public void AttributeSuffixMatch(string name, string value)
	    {
            Add(string.Format(" which have a {0} attribute whose value ends with '{1}'", name, value));
        }

	    /// <summary>
	    /// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
	    /// that represents an element with the attribute <paramref name="name"/> 
	    /// whose value contains at least one instance of the substring <paramref name="value"/>.
	    /// </summary>
	    public void AttributeSubstring(string name, string value)
	    {
            Add(string.Format(" which have a {0} attribute whose value contains '{1}'", name, value));
        }

	    /// <summary>
		/// Generates a <a href="http://www.w3.org/TR/css3-selectors/#pseudo-classes">pseudo-class selector</a>,
		/// which represents an element that is the first child of some other element.
		/// </summary>
		public void FirstChild()
		{
			Add(" where the node is the first child");
		}

		/// <summary>
		/// Generates a <a href="http://www.w3.org/TR/css3-selectors/#pseudo-classes">pseudo-class selector</a>,
		/// which represents an element that is the last child of some other element.
		/// </summary>
		public void LastChild()
		{
			Add(" where the node is the last child");
		}

		/// <summary>
		/// Generates a <a href="http://www.w3.org/TR/css3-selectors/#pseudo-classes">pseudo-class selector</a>,
		/// which represents an element that is the N-th child of some other element.
		/// </summary>
		public void NthChild(int position)
		{
			Add(string.Format(" where the node is the {0}th child", position));
		}

		/// <summary>
		/// Generates a <a href="http://www.w3.org/TR/css3-selectors/#pseudo-classes">pseudo-class selector</a>,
		/// which represents an elementthat has a parent element and whose parent 
		/// element has no other element children.
		/// </summary>
		public void OnlyChild()
		{
			Add(" where the node is the only child");
		}

		/// <summary>
		/// Generates a <a href="http://www.w3.org/TR/css3-selectors/#pseudo-classes">pseudo-class selector</a>,
		/// which represents an element that has no children at all.
		/// </summary>
		public void Empty()
		{
			Add(" where the node is empty");
		}

		/// <summary>
		/// Generates a <a href="http://www.w3.org/TR/css3-selectors/#combinators">combinator</a>,
		/// which represents a childhood relationship between two elements.
		/// </summary>
		public void Child()
		{
			Add(" child of");
		}

		/// <summary>
		/// Generates a <a href="http://www.w3.org/TR/css3-selectors/#combinators">combinator</a>,
		/// which represents a relationship between two elements where one element is an 
		/// arbitrary descendant of some ancestor element.
		/// </summary>
		public void Descendant()
		{
			if (_chainCount > 0)
			{
				Add(", which in turn have descendants");
			}
			else
			{
				Add(" which have descendants");
				_chainCount++;
			}
		}

		/// <summary>
		/// Generates a <a href="http://www.w3.org/TR/css3-selectors/#combinators">combinator</a>,
		/// which represents elements that share the same parent in the document tree and 
		/// where the first element immediately precedes the second element.
		/// </summary>
		public void Adjacent()
		{
			Add(" which is immediately preceeded by a sibling node");
		}
	}
}