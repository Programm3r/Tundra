using System;
using Tundra.Interfaces.Presenter;
using Tundra.Interfaces.View;

namespace Tundra.MVP.Presenter
{
    /// <summary>
    /// Presenter base class declaration
    /// </summary>
    /// <typeparam name="TView">The type of the view.</typeparam>
    /// <typeparam name="TProcessor">The type of the processor.</typeparam>
    /// <example>
    /// The following example demonstrates how to implement the <see cref="PresenterBase{TView,TProcessor}"/>.
    /// <code>
    /// public class PersonData
    /// {
    ///     public string time { get; set; }
    ///     public long milliseconds_since_epoch { get; set; }
    ///     public string date { get; set; }
    /// }
    /// 
    /// public interface IPersonView : IView
    /// {
    ///     PersonData PersonDataProperty { get; set; }
    /// }
    /// 
    /// public class PersonView : ViewBase, IPersonView
    /// {
    ///     private read-only PersonPresenter _presenter;
    ///     private PersonData _personData = new PersonData();        
    /// 
    ///     public PersonData PersonDataProperty
    ///     {
    ///         get
    ///         {
    ///             return this._personData;
    ///         }
    ///         set
    ///         {
    ///             this._personData = value;
    ///             // bind to UI controls here
    ///         }
    ///     }
    /// 
    ///     public PersonView()
    ///     {
    ///         // create a new instance of the person presenter
    ///         this._presenter = new PersonPresenter(this, new HttpClientWrapper());
    ///         // this event will fire in the presenter, because of the HookupViewEvents method
    ///         base.OnDataRetrieved(new EventArgs());
    ///     }
    /// 
    ///     protected override void OnUserInformationChanged(string userInformation)
    ///     {
    ///         // bind to UI controls here to show the user information
    ///         Trace.TraceInformation("UserInformation: {0}", userInformation);
    ///     }
    /// 
    ///     protected override void Dispose(bool disposing)
    ///     {            
    ///         if (disposing)
    ///         {
    ///             // free managed resources
    ///             this._presenter.Dispose();
    ///             Trace.TraceInformation("Dispose was called on Presenter");
    ///         }
    ///         // free native resources if there are any.
    ///     }
    /// }
    /// 
    /// public class PersonPresenter : PresenterBase&lt;IPersonView, IHttpClientDataProvider&gt;
    /// {
    ///     public PersonPresenter(IPersonView personView, IHttpClientDataProvider dataProvider)
    ///         : base(personView, dataProvider)
    ///     {
    ///         this.HookupViewEvents(personView);
    ///     }
    /// 
    ///     private void ViewOnRetrievePersonDataHandler(object sender, EventArgs eventArgs)
    ///     {
    ///         this.View.UserInformation = "Loading person data...";
    ///         Response&lt;PersonData&gt; personData = base.Processor.InvokeGetAsync&lt;PersonData&gt;("http://date.jsontest.com").Result;
    ///         if (personData.IsSuccess &amp;&amp; personData.Data != null)
    ///         {
    ///             // this will update the property in the view and 
    ///             // result in updating the UI
    ///             this.View.PersonDataProperty = personData.Data;
    ///             Trace.TraceInformation("Retrieved data from Processor");
    ///         }
    ///         this.View.UserInformation = "Loading completed...";
    ///     }
    /// 
    ///     protected override sealed void HookupViewEvents(IPersonView view)
    ///     {            
    ///         view.RetrieveDataEventHandler += ViewOnRetrievePersonDataHandler;
    ///         Trace.TraceInformation("HookupViewEvents was called");
    ///     }
    /// 
    ///     protected override void UnhookViewEvents(IPersonView view)
    ///     {
    ///         view.RetrieveDataEventHandler -= ViewOnRetrievePersonDataHandler;
    ///         Trace.TraceInformation("UnhookViewEvents was called");
    ///     }
    /// }
    /// </code>
    /// </example>
    public abstract class PresenterBase<TView, TProcessor> : IPresenterBase<TView, TProcessor>, IDisposable
        where TView : IView
        where TProcessor : class
    {
        #region PresenterBase - CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="PresenterBase{TView, TProcessor}" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="processor">The processor.</param>
        protected PresenterBase(TView view, TProcessor processor)
        {
            this.Processor = processor;
            this.View = view;
        }

        #endregion

        #region IPresenterBase - Implementation

        /// <summary>
        /// Gets the processor.
        /// </summary>
        /// <value>
        /// The processor.
        /// </value>
        public TProcessor Processor { get; private set; }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        public TView View { get; private set; }

        #endregion

        #region PresenterBase - Public Methods

        /// <summary>
        /// Disposes the view.
        /// </summary>
        /// <param name="view">The view.</param>
        public void DisposeView(TView view)
        {
            this.UnhookViewEvents(view);
        }

        /// <summary>
        /// Refreshes the view.
        /// </summary>
        /// <param name="view">The view.</param>
        public void RefreshView(TView view)
        {
            this.View = view;
            this.HookupViewEvents(this.View);
        }

        #endregion

        #region PresenterBase - Virtual Methods

        /// <summary>
        /// Hook-ups the view events.
        /// </summary>
        /// <param name="view">The view.</param>
        protected virtual void HookupViewEvents(TView view)
        {
        }

        /// <summary>
        /// Unhook the events within the view.
        /// </summary>
        /// <param name="view">The view.</param>
        protected virtual void UnhookViewEvents(TView view)
        {
        }

        #endregion

        #region IDisposable - Implementation

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="PresenterBase{TView, TProcessor}"/> is reclaimed by garbage collection.
        /// </summary>
        ~PresenterBase()
        {
            // Finalize calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                this.UnhookViewEvents(this.View);

                var disposable = this.Processor as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            // free native resources if there are any.
        }

        #endregion
    }
}