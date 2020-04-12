namespace LotteryBook.Model.Settings
{
    public class LotterySettings : NotifyPropertyChangedBase
    {
        private bool m_AnimateDrawing;

        public LotterySettings()
        {
            AnimateDrawing = true;
        }

        public bool AnimateDrawing
        {
            get
            {
                return m_AnimateDrawing;
            }
            set
            {
                m_AnimateDrawing = value;
                OnPropertyChanged("AnimateDrawing");
            }
        }
    }
}