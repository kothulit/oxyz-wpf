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

            _maessenger.Subscribe<GeometryEventArgs>(EventEnum.HitToGeometryModel.ToString(), OnHitToGeometryModel);
            _maessenger.Subscribe<InstructionEventArgs>(EventEnum.InstructionCanseled.ToString(), OnCanceled);
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
            _maessenger.Publish(EventEnum.SelectionChange.ToString(), this, new SelectionChangeEventArgs(SelectionIds));
        }

        public void OnHitToGeometryModel(object? _, GeometryEventArgs e)
        {
            var model = e.GeometryModel;

            foreach (var entity  in _projectWorld.GetEntitiesWithComponents<MeshComponent>())
            {
                var mesh = entity.GetComponent<MeshComponent>();
                if (mesh?.Geometry == model.Geometry)
                {
                    Add(entity.Id);
                }
            }
        }

        public void OnCanceled(object? _, InstructionEventArgs e)
        {
            Clear();
        }
    }
}
