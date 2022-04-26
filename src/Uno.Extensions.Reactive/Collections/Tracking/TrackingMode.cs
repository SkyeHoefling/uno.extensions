﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Uno.Extensions.Collections.Tracking;

/// <summary>
/// The modes that can be used to track changes between collections
/// </summary>
internal enum TrackingMode
{
	/// <summary>
	/// The collection determines by itself if its use the <see cref="Smart"/> or the <see cref="Reset"/> mode.
	/// <remarks>The collection will use reset if there is any handler of the <see cref="INotifyCollectionChanged.CollectionChanged"/> event.</remarks>
	/// </summary>
	Auto = 0,

	/// <summary>
	/// The collection determines the changes from the previous version and notifies changes item per item.
	/// </summary>
	Smart = 1,

	/// <summary>
	/// The collection raise a unique <see cref="NotifyCollectionChangedAction.Reset"/>.
	/// </summary>
	Reset = 2,
}