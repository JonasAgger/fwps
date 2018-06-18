using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Opgave1.Annotations;

namespace Opgave1
{
    [Serializable]
    public class VarroCount : INotifyPropertyChanged
    {
        private string _bistade;
        private DateTime _time;
        private uint _count;
        private string _comment;


        public VarroCount()
        {
            _time = DateTime.Now.Date;
        }
        public VarroCount(string bistade, DateTime time, uint count, string comment)
        {
            _bistade = bistade;
            this._time = time;
            this._count = count;
            this._comment = comment;
        }

        public string Bistade
        {
            get
            {
                return _bistade;
            }
            set
            {
                if (_bistade != value)
                {
                    _bistade = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public uint Count
        {
            get
            {
                return _count;
            }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
