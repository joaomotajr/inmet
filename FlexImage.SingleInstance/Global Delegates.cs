// ============================================
// 
// FlexImage.SingleInstance
// Global Delegates.cs
// 26/08/2012
// cflavio
// 
// ============================================

namespace FlexImage.SingleInstance
{
    /// <summary>
    ///   Represents the method which would be used to retrieve an ISingleInstanceEnforcer object when instantiating a SingleInstanceTracker object.
    ///   If the method returns null, the SingleInstanceTracker's constructor will throw an exception.
    /// </summary>
    /// <returns> An ISingleInstanceEnforcer object which would receive messages. </returns>
    public delegate ISingleInstanceEnforcer SingleInstanceEnforcerRetriever();
}