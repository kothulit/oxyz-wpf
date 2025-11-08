using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.ECS.Components;

namespace OxyzWPF.Game
{
    public class Selection : ISelection
    {
        private IMessenger _maessenger;
        private IWorld _world;
        
        public List<int> SelectionIds {  get; private set; }

        public Selection(IMessenger messenger, IWorld world)
        {
            _maessenger = messenger;
            _world = world;

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

        public void OnHitToGeometryModel(object sender, GeometryEventArgs e)
        {
            var model = e.GeometryModel;

            foreach (var entity  in _world.GetEntitiesWithComponents<MeshComponent>())
            {
                var mesh = entity.GetComponent<MeshComponent>();
                if (mesh?.Geometry == model.Geometry)
                {
                    Add(entity.Id);
                }
            }
        }

        public void OnCanceled(object sender, InstructionEventArgs e)
        {
            Clear();
        }
    }
}
