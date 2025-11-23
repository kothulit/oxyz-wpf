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
            _maessenger.Subscribe<SelectionChangeEventArgs>(EventEnum.SelectionChange.ToString(), OnSelectionChange);
        }

        private void OnSelectionChange(object? _, SelectionChangeEventArgs e)
        {
            SelectionIds = e.SelectionIds;
        }

        public void OnCanceled(object? _, EventArgs e)
        {
            SelectionIds.Clear();
        }
    }
}
