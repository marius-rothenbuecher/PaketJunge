using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PaketJunge.View
{
	public class ViewTemplateSelector : DataTemplateSelector
	{
        private const string ViewNameEnding = "View";
        private const string ViewModelNameEnding = "ViewModel";

        private static Dictionary<Type, DataTemplate> templates = null;
        
		static ViewTemplateSelector()
		{
			templates = new Dictionary<Type, DataTemplate>();
			templates = typeof(ViewTemplateSelector)
				.Assembly.GetTypes()
				.Where(x => x.Name.EndsWith(ViewNameEnding))
				.Where(x => x.IsSubclassOf(typeof(FrameworkElement)))
				.ToDictionary(x => x, x => new DataTemplate(x) { VisualTree = new FrameworkElementFactory(x) });
		}

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
            string viewTypeName = string.Empty;

            if (item != null)
                viewTypeName = item.GetType().Name.Replace(ViewModelNameEnding, ViewNameEnding);
            else
                viewTypeName = $"Empty{((ContentPresenter)container).Name}{ViewNameEnding}";

            var viewType = Type.GetType(string.Concat(this.GetType().Namespace, ".", viewTypeName));

            if (viewType == null)
				return null;

			var template = templates[viewType];

			if (template != null)
				return template;

			return base.SelectTemplate(item, container);
		}
	}
}
