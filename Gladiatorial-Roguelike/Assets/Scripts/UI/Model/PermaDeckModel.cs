namespace UI.Model
{
    public class PermaDeckModel
    {
        public bool HasContinueBtn { get; private set; }

        public void SetHasContinueBtn(bool hasContinueBtn) => 
            HasContinueBtn = hasContinueBtn;
    }
}