namespace Microservices.Models
{
    public class ServiceId
    {
        private readonly string _svcName;
        private readonly string _id;

        public ServiceId(string svcName, string id)
        {
            _svcName = svcName;
            _id = id;
        }

        internal ServiceId(string identifier)
        {
            var index = identifier.LastIndexOf('_');

            _svcName = identifier.Substring(0, index);
            _id = identifier.Substring(index + 1);
        }

        public string Identifier
        {
            get { return string.Format("{0}_{1}", _svcName, _id); }
        }

        public override string ToString()
        {
            return Identifier;
        }

        public string GetName
        {
            get { return _svcName; }
        }

        protected bool Equals(ServiceId other)
        {
            return string.Equals(_svcName, other._svcName) && string.Equals(_id, other._id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ServiceId)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_svcName != null ? _svcName.GetHashCode() : 0) * 397) ^ (_id != null ? _id.GetHashCode() : 0);
            }
        }
    }
}