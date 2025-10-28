using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.Game
{
    public class Selection : ISelection
    {
        private IMailer _mailer;
        public List<int> SelectionIds {  get; private set; }

        public Selection(IMailer mailer)
        {
            _mailer = mailer;
        }
        public void Add(int id)
        {
            SelectionIds.Add(id);
            OnSelectionChange();
        }

        public void Remove(int id)
        {
            SelectionIds.Remove(id);
            OnSelectionChange();
        }

        public void Clear()
        {
            SelectionIds.Clear();
            OnSelectionChange();
        }

        private void OnSelectionChange()
        {
            _mailer.Publish(EventEnum.SelectionChange, SelectionIds);
        }
    }
}
