using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.Domain.Framework.SeedWork {
    public interface IEntity<TId> : INotifyPropertyChanged, INotifyPropertyChanging {
        TId Id { get; set; }

        string Name { get; set; }
    }
}
