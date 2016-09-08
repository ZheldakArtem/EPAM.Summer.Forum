using BLL;
using BLL.Interface;
using BLL.Interface.Services;
using DAL;
using DAL.Interface.Repository;
using DAL.NLog;
using Ninject;
using Ninject.Web.Common;
using ORM;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EPAM.SUMMER.FORUM.ZHELDAK.NinjectConfig), "Start")]
namespace EPAM.SUMMER.FORUM.ZHELDAK
{
    public static class NinjectConfig
    {
        private static IKernel _kernel;
        public static IKernel StandartKernel => _kernel;
        public static bool StartCalled { get; set; }

        public static void Start()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IRepository<User>>().To<UserRepository>().InRequestScope();
            _kernel.Bind<IRepository<Role>>().To<RoleRepository>().InRequestScope();
            _kernel.Bind<IRepository<Question>>().To<QuestionRepository>().InRequestScope();
            _kernel.Bind<IRepository<Category>>().To<CategoryRepository>().InRequestScope();
            _kernel.Bind<IRepository<Comment>>().To<CommentRepository>().InRequestScope();
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            _kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            _kernel.Bind<IRoleService>().To<RoleService>().InRequestScope();
            _kernel.Bind<IQuestionService>().To<QuestionService>().InRequestScope();
            _kernel.Bind<ICommentService>().To<CommentService>().InRequestScope();
            _kernel.Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            _kernel.Bind<ILogForum>().To<LogForum>().InSingletonScope();
            StartCalled = true;
        }
    }
}