namespace ClinicTool.DataAccess.Entities
{
    public class ValueHolder<T>
    {
        private T value;
        private readonly Func<object, T> valueRetrieval;

        public ValueHolder(Func<object, T> valueRetrieval)
        {
            this.valueRetrieval = valueRetrieval;
        }

        public T GetValue(object parameter)
        {
            if (value == null)
                value = valueRetrieval(parameter);

            return value;
        }
    }
}


