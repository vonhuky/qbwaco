package crc64ee03036c0e48a064;


public class ObservableScrollView
	extends android.widget.ScrollView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onScrollChanged:(IIII)V:GetOnScrollChanged_IIIIHandler\n" +
			"";
		mono.android.Runtime.register ("ODAMobile.Droid.Controls.FloatingActionButton.ObservableScrollView, ODAMobile.Android", ObservableScrollView.class, __md_methods);
	}


	public ObservableScrollView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ObservableScrollView.class)
			mono.android.TypeManager.Activate ("ODAMobile.Droid.Controls.FloatingActionButton.ObservableScrollView, ODAMobile.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public ObservableScrollView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == ObservableScrollView.class)
			mono.android.TypeManager.Activate ("ODAMobile.Droid.Controls.FloatingActionButton.ObservableScrollView, ODAMobile.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public ObservableScrollView (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == ObservableScrollView.class)
			mono.android.TypeManager.Activate ("ODAMobile.Droid.Controls.FloatingActionButton.ObservableScrollView, ODAMobile.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public ObservableScrollView (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == ObservableScrollView.class)
			mono.android.TypeManager.Activate ("ODAMobile.Droid.Controls.FloatingActionButton.ObservableScrollView, ODAMobile.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public void onScrollChanged (int p0, int p1, int p2, int p3)
	{
		n_onScrollChanged (p0, p1, p2, p3);
	}

	private native void n_onScrollChanged (int p0, int p1, int p2, int p3);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
