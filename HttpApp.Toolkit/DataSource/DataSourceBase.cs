using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace HttpApp.Toolkit.DataSource
{
    public delegate void OnDataRequestError(int code);

    /// <summary>
    /// 增量加载泛型集合类基类
    /// </summary>
    public abstract class DataSourceBase<T> : IncrementalLoadingBase<T>
    {
        /// <summary>
        /// 如果想刷新数据，让数据重新从第一页加载，使用此方法
        /// </summary>
        public async virtual Task Refresh()
        {
            //reset
            this._currentPage = 1;
            this._hasMoreItems = true;

            this.Clear();
            await LoadMoreItemsAsync(20);
        }

        protected DateTime _lastTime = DateTime.MinValue;

        /// <summary>
        /// 设置请求间隔，0.5s内被认为是一次请求
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsInTime()
        {
            var delta = DateTime.Now - _lastTime;
            _lastTime = DateTime.Now;
            return delta.TotalMilliseconds < 500;
        }

        /// <summary>
        /// 内容是分页的，所以这里特别处理
        /// </summary>
        protected override async Task<IList<T>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            if (IsInTime())
            {
                return null;
            }

            var newItems = await this.LoadItemsAsync();

            //更新pageIndex
            if (newItems != null)
            {
                _currentPage++;
            }

            //判断是否有新的内容
            this._hasMoreItems = newItems != null && newItems.Count > 0;

            return newItems;
        }

        protected void FireErrorEvent(int code)
        {
            if (this.DataRequestError != null)
            {
                this.DataRequestError(code);
            }
        }

        public event OnDataRequestError DataRequestError;

        protected override bool HasMoreItemsOverride()
        {
            return _hasMoreItems;
        }

        protected abstract Task<IList<T>> LoadItemsAsync();

        protected int _currentPage = 1;
        protected bool _hasMoreItems = true;
    }
}
