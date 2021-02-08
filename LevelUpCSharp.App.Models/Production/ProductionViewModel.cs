using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace LevelUpCSharp.Production
{
    public class ProductionViewModel
    {
        private readonly ProductionService _productionService;
        private readonly List<VendorViewModel> _vendors;

        public ProductionViewModel(ProductionService productionService)
        {
            _productionService = productionService;
            Add = new RelayCommand<string>(NewConsumer);
            _vendors = InitializeVendors();
        }

        public ICommand Add { get; }

        public IEnumerable<VendorViewModel> Vendors => _vendors;

        private void NewConsumer(string name)
        {
            var vendor = _productionService.Create(name);
            _vendors.Add(new VendorViewModel(vendor));
        }

        private List<VendorViewModel> InitializeVendors()
        {
            var consumers = new List<VendorViewModel>();
            foreach (var consumer in Repositories.Vendors.GetAll())
            {
                consumers.Add(new VendorViewModel(consumer));
            }

            return new List<VendorViewModel>(consumers);
        }
    }
}