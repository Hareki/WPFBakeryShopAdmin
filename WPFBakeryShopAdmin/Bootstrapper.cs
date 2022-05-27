using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using WPFBakeryShopAdmin.MVVM.ViewModels;
using WPFBakeryShopAdmin.ViewModels;

namespace WPFBakeryShopAdmin
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.PerRequest<ForgotPasswordViewModel>();
            _container.PerRequest<AddingAccountViewModel>();
            _container.PerRequest<AddingProductViewModel>();
            _container.PerRequest<VariantAddEditDialogViewModel>();

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            _container.Singleton<LoginViewModel>();
            _container.Singleton<MainViewModel>();


            _container.Singleton<DashboardViewModel>();

            _container.Singleton<ProductViewModel>();
           
            _container.Singleton<BillViewModel>();
            _container.Singleton<AccountViewModel>();
            _container.Singleton<PersonalAccountViewModel>();



            //_container.Singleton<PersonalAccount>();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<LoginViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
