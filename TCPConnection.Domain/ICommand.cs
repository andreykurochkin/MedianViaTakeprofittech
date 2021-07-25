using System.Threading.Tasks;

namespace TCPConnection.Domain {
    public interface ICommand<TResult> {
        public Task<TResult> ExecuteAsync();
    }
}
