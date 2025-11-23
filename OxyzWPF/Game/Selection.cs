using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.ECS.Components;
using OxyzWPF.ECS;

namespace OxyzWPF.Game
{
    public class Selection : ISelection
    {
        private IMessenger _maessenger;
        private ProjectWorld _projectWorld;
        
        public List<int> SelectionIds {  get; private set; }

        public Selection(IMessenger messenger, ProjectWorld projectWorld)
        {
            _maessenger = messenger;
            _projectWorld = projectWorld;

            SelectionIds = new List<int>();

            _maessenger.Subscribe<EventArgs>(EventEnum.Сancellation.ToString(), OnCanceled);
            _maessenger.Subscribe<SelectionChangeCallEventArgs>(EventEnum.SelectionChangeCall.ToString(), OnSelectionChange);
        }

        private void OnSelectionChange(object? _, SelectionChangeCallEventArgs e)
        {
            SelectionIds = e.SelectedElementsIds;
        }

        public void OnCanceled(object? _, EventArgs e)
        {
            SelectionIds.Clear();
        }
    }
}
