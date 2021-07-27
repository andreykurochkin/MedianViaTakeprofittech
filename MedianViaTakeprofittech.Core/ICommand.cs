using System.Threading.Tasks;

namespace MedianViaTakeprofittech.Application {
    public interface ICommand<TResult> {
        public Task<TResult> ExecuteAsync();
    }
}
