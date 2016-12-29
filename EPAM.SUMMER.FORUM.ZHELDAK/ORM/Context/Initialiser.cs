using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Context
{
    public class ItemsInitializer : DropCreateDatabaseAlways<EntityModelContext>
    {
        protected override void Seed(EntityModelContext db)
        {
            //db.Items.Add(new Item { Tittle = "task 1", Author = "Л. Толстой", Task = ".....", Id = 1, CreateDate = DateTime.Now });
            //db.Items.Add(new Item { Tittle = "task 2", Author = "И. Тургенев", Task = ".....", Id = 2, CreateDate = DateTime.Now });
            //db.Items.Add(new Item { Tittle = "task 3", Author = "А. Чехов", Task = ".....", Id = 3, CreateDate = DateTime.Now });

            base.Seed(db);
        }
    }
}
