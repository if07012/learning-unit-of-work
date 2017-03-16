using Sample.Domains;
using Sample.Domains.Helpers;
using Sample.Domains.Repositories;
using Sample.Impls;
using Sample.Impls.Commands;
using Sample.Impls.Context;
using Sample.Impls.Repositories;
using Sample.Presenters;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sample.MVP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Container container;
        public App()
        {
            container = new Container();
            container.RegisterSingleton<IContextWithTransaction, FileContext>();
            container.RegisterSingleton<IUnitOfWork, UnitOfWork>();
            container.Register<IUserRepository, UserRepository>();
            container.Register<IMessageRepository, MessageRepository>();
            container.Verify();
            Helpers.SetContainer(container);


        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var view = new MainWindow();
            UserPresenter userPresenter = new UserPresenter(view, new UserCommand(view, container.GetInstance<IUnitOfWork>()));
            this.MainWindow = view;
            view.Show();

        }
    }
}
