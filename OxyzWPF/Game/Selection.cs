using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Components;

namespace OxyzWPF.Game
{
    public class Selection : ISelection
    {
        private IMailer _mailer;
        private IWorld _world;
        
        public List<int> SelectionIds {  get; private set; }

        public Selection(IMailer mailer, IWorld world)
        {
            _mailer = mailer;
            _world = world;

            SelectionIds = new List<int>();

            _mailer.Subscribe<object>(EventEnum.HitToGeometryModel, OnHitToGeometryModel);
            _mailer.Subscribe<object>(EventEnum.InstructionCanseled, OnCanceled);
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

        public void OnHitToGeometryModel(object args)
        {
            var model = (args as MeshGeometryModel3D);

            foreach (var entity  in _world.GetEntitiesWithComponents<MeshComponent>())
            {
                var mesh = entity.GetComponent<MeshComponent>();
                if (mesh?.Geometry == model.Geometry)
                {
                    Add(entity.Id);
                }
            }
        }

        public void OnCanceled(object args)
        {
            Clear();
        }
    }
}
