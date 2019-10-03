using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Moq;

namespace ZZZTest.prioritizeMeServices
{
        public abstract class ClassWithDbContextFactory<T>
            where T : DbContext
        {
            /// <summary>
            /// The mock <see cref="IDesignTimeDbContextFactory{TContext}" />
            /// </summary>
            protected readonly Mock<IDesignTimeDbContextFactory<T>> ContextFactoryMock;

            /// <summary>
            /// Sets up the <see cref="ContextFactoryMock" /> for use by sub classes
            /// </summary>
            protected ClassWithDbContextFactory()
            {
                ContextFactoryMock = new Mock<IDesignTimeDbContextFactory<T>>();

                DbContextOptions<T> dbContextOptions =
                    new DbContextOptionsBuilder<T>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;

                ContextFactoryMock.Setup(m => m.CreateDbContext(It.IsAny<string[]>()))
                    .Returns(() => (T)Activator.CreateInstance(typeof(T), dbContextOptions));
            }

            /// <summary>
            /// Gets a copy of the <see cref="T" /> the way it's retrieved in the repo classes
            /// </summary>
            /// <returns>The in memory <see cref="T" /></returns>
            protected T GetTestContext()
            {
                return ContextFactoryMock.Object.CreateDbContext(new string[0]);
            }
        }
}
