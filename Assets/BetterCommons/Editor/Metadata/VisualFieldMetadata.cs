using UnityEngine.UIElements;

namespace Better.Commons.EditorAddons.Metadata
{
	public class VisualFieldMetadata
	{
		public Foldout FieldFoldout { get; }
		public bool HasFoldout { get; }

		public Label FieldLabel { get; }
		public bool HasFieldLabel { get; }

		public VisualElement Field { get; }
		public bool HasField { get; }

		public VisualElement FieldInput { get; }
		public bool HasFieldInput { get; }

		public VisualFieldMetadata(VisualElement sourceElement)
		{
			FieldFoldout = sourceElement.Query()
				.Descendents<Foldout>()
				.First();

			HasFoldout = FieldFoldout != null;

			if (HasFoldout)
			{
				Field = FieldFoldout.Query<Toggle>()
					.Class(BaseField<object>.ussClassName)
					.Class(Foldout.toggleUssClassName)
					.First();
			}
			else
			{
				Field = sourceElement.Query()
					.Class(BaseField<object>.ussClassName)
					.Where(element => element != FieldFoldout)
					.First();
			}

			HasField = Field != null;

			if (HasField)
			{
				FieldLabel = Field.Q<Label>();
				HasFieldLabel = FieldLabel != null;

				FieldInput = Field.Query()
					.Class(BaseField<object>.inputUssClassName)
					.First();

				HasFieldInput = FieldInput != null;
			}
		}
	}
}