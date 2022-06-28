package crc64ee03036c0e48a064;


public class FloatingActionButton_MyOutlineProvider
	extends android.view.ViewOutlineProvider
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getOutline:(Landroid/view/View;Landroid/graphics/Outline;)V:GetGetOutline_Landroid_view_View_Landroid_graphics_Outline_Handler\n" +
			"";
		mono.android.Runtime.register ("ODAMobile.Droid.Controls.FloatingActionButton.FloatingActionButton+MyOutlineProvider, ODAMobile.Android", FloatingActionButton_MyOutlineProvider.class, __md_methods);
	}


	public FloatingActionButton_MyOutlineProvider ()
	{
		super ();
		if (getClass () == FloatingActionButton_MyOutlineProvider.class)
			mono.android.TypeManager.Activate ("ODAMobile.Droid.Controls.FloatingActionButton.FloatingActionButton+MyOutlineProvider, ODAMobile.Android", "", this, new java.lang.Object[] {  });
	}

	public FloatingActionButton_MyOutlineProvider (android.content.res.Resources p0, int p1)
	{
		super ();
		if (getClass () == FloatingActionButton_MyOutlineProvider.class)
			mono.android.TypeManager.Activate ("ODAMobile.Droid.Controls.FloatingActionButton.FloatingActionButton+MyOutlineProvider, ODAMobile.Android", "Android.Content.Res.Resources, Mono.Android:Xamarin.Forms.Controls.FabSize, ODAMobile", this, new java.lang.Object[] { p0, p1 });
	}


	public void getOutline (android.view.View p0, android.graphics.Outline p1)
	{
		n_getOutline (p0, p1);
	}

	private native void n_getOutline (android.view.View p0, android.graphics.Outline p1);

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
