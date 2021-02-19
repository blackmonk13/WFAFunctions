using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFAFunctions.Common;
using Timer = System.Windows.Forms.Timer;

namespace WFAFunctions.UI
{
    public class RoundedControl : Component
    {
		private ContainerControl containerControl;
		private Control controler;
		private IContainer iContain;
		private Timer timer;

		private int radius = 6;

		private EventHandler RHandler;

		private Control TargetControl
        {
            get
            {
				return this.controler;
            }
            set
            {
				this.controler = value;
            }
        }

		public int CurveRadius
		{
			get
			{
				return this.radius;
			}
			set
			{
				this.radius = value;
				this.ApplyCurve();
			}
		}

		private ContainerControl ContainerControl
		{
			get
			{
				return this.containerControl;
			}
			set
			{
				this.containerControl = value;
				this.ApplyCurve();
			}
		}

		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (value == null)
				{
					return;
				}
				IDesignerHost designerHost = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					IComponent rootComponent = designerHost.RootComponent;
					if (rootComponent is ContainerControl)
					{
						this.ContainerControl = (rootComponent as ContainerControl);
						return;
					}
				}
			}
		}

		public RoundedControl()
        {
			Load();
			if (this.TargetControl == null)
			{
				this.TargetControl = this.ContainerControl;
			}
		}

		public RoundedControl(IContainer container)
		{
			container.Add(this);
			this.Load();
		}

		private void Load()
		{
			this.iContain = new Container();
			this.timer = new Timer(this.iContain);
			this.timer.Enabled = true;
			this.timer.Tick += this.onTimer_tick;
		}

		private void onTimer_tick(object sender, EventArgs e)
        {
            try
            {
				this.timer.Stop();
                if (controler != null)
                {
					controler.Resize += onResize;
                }
                else
                {
					this.controler = this.ContainerControl;
					controler.Resize += onResize;
                }

                if (controler.GetType() == typeof(Form))
                {
					((Form)controler).FormBorderStyle = FormBorderStyle.None;
                }
				ApplyCurve();
            }
            catch (Exception)
            {
				this.timer.Start();
            }
        }

		private void onResize(object sender, EventArgs e)
        {
			Rounded.Apply(this.controler, this.radius);
            if (this.RHandler != null)
            {
				this.RHandler(sender, e);
            }
        }

		public event EventHandler TargetControlResized
		{
			add
			{
				EventHandler eventHandler = RHandler;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.RHandler, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = RHandler;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.RHandler, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public void ApplyCurve(int Radius)
		{
			if (this.controler != null)
			{
				Rounded.Apply(this.controler, Radius);
			}
			
		}

		public void ApplyCurve()
		{
			try
			{
				if (this.controler != null)
				{
					Rounded.Apply(this.controler, this.radius);
				}
			}
			catch (Exception)
			{
			}
			
		}

		public void ApplyCurve(Control control, int Radius)
		{
			if (control != null)
			{
				Rounded.Apply(control, Radius);
			}
		}

		public void ApplyCurve(Control control)
		{
			if (control != null)
			{
				Rounded.Apply(control, this.radius);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.iContain != null)
			{
				this.iContain.Dispose();
			}
			base.Dispose(disposing);
		}

		
    }
}
