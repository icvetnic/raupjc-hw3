using System.Data.Entity;

namespace Assignment1
{
    public class TodoDbContext : DbContext
    {
        public IDbSet<TodoItem> TodoItems { get; set; }
        public IDbSet<TodoItemLabel> TodoItemLabels { get; set; }

        public TodoDbContext(string conectionString) : base(conectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>().HasKey(td => td.Id);
            modelBuilder.Entity<TodoItem>().Property(td => td.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(td => td.IsCompleted).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(td => td.DateCompleted).IsOptional();
            modelBuilder.Entity<TodoItem>().Property(td => td.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(td => td.UserId).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(td => td.DateDue).IsOptional();
            modelBuilder.Entity<TodoItem>().HasMany(td => td.Labels).WithMany(tdl => tdl.LabelTodoItems);

            modelBuilder.Entity<TodoItemLabel>().HasKey(tdl => tdl.Id);
            modelBuilder.Entity<TodoItemLabel>().Property(tdl => tdl.Value).IsRequired();

        }
    }
}