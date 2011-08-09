using System;
using DeployerEngine.Database;
using DeployerPluginInterfaces;

namespace DeployerEngine
{
	public delegate void DeployMessageHandler(string message);
	public delegate void DeployFileTransferHandler(TransferEventArgs e);
	public delegate void DeployDatabaseTransferHandler(DatabaseTransferEventArgs e);
	public delegate void DeployTableFoundHandler(string tablename);
	public delegate void DeployTableComparisonCompleteHandler(TableComparison comparison);

	/// <summary>
	/// Handles events from the deployer engine. Can be used to listen to events that happen.
	/// </summary>
	public class EventManager
	{
		public static event DeployMessageHandler CommandSent;
		public static event DeployMessageHandler ReplyReceived;
		public static event DeployFileTransferHandler TransferBegin;
		public static event DeployFileTransferHandler TransferProgress;
		public static event DeployFileTransferHandler TransferComplete;
		public static event DeployDatabaseTransferHandler DatabaseTransferBegin;
		public static event DeployDatabaseTransferHandler DatabaseTransferComplete;

		/// <summary>
		/// Occurs when verifying a databases when a new source table is found.
		/// </summary>
		public static event DeployTableFoundHandler SourceTableFound;

		/// <summary>
		/// Occurs when a table has been compared during database verification.
		/// </summary>
		public static event DeployTableComparisonCompleteHandler TableComparisonComplete;

		/// <summary>
		/// Occurs when the engine wants to notify its client about an event.
		/// </summary>
		public static event DeployMessageHandler NotificationMessage;

		//public static event EventHandler ProjectChanged;

		private EventManager()
		{
		}

		/// <summary>
		/// Signals the CommandSent event.
		/// </summary>
		/// <param name="message">The message sent to the server.</param>
		internal static void OnCommandSent(string message) {
			if(CommandSent != null)
				CommandSent(message);
		}

		/// <summary>
		/// Signals the DatabaseTransferBegin event.
		/// </summary>
		internal static void OnDatabaseTransferBegin(DatabaseTransferEventArgs e) {
			if(DatabaseTransferBegin != null)
				DatabaseTransferBegin(e);
		}

		/// <summary>
		/// Signals the DatabaseTransferComplete event.
		/// </summary>
		internal static void OnDatabaseTransferComplete(DatabaseTransferEventArgs e) {
			if(DatabaseTransferComplete != null)
				DatabaseTransferComplete(e);
		}

		/// <summary>
		/// Signals the NotificationMessage event.
		/// </summary>
		/// <param name="message"></param>
		internal static void OnNotificationMessage(string message) {
			if(NotificationMessage != null)
				NotificationMessage(message);
		}

		/// <summary>
		/// Signals the ReplyReceived event.
		/// </summary>
		/// <param name="message"></param>
		internal static void OnReplyReceived(string message) {
			if(ReplyReceived != null)
				ReplyReceived(message);
		}

		/// <summary>
		/// Signals the ProjectChanged event.
		/// </summary>
		//internal static void OnProjectChanged() {
		//    if(ProjectChanged != null)
		//        ProjectChanged(null, new EventArgs());
		//}

		/// <summary>
		/// Signals the SourceTableFound event.
		/// </summary>
		/// <param name="tablename"></param>
		internal static void OnSourceTableFound(string tablename) {
			if(SourceTableFound != null)
				SourceTableFound(tablename);
		}

		/// <summary>
		/// Signals the TableComparisonComplete event.
		/// </summary>
		/// <param name="comparison"></param>
		internal static void OnTableComparisonComplete(TableComparison comparison) {
			if(TableComparisonComplete != null)
				TableComparisonComplete(comparison);
		}

		/// <summary>
		/// Signals the OnTransferBegin event.
		/// </summary>
		/// <param name="e"></param>
		internal static void OnTransferBegin(TransferEventArgs e) {
			if(TransferBegin != null)
				TransferBegin(e);
		}

		/// <summary>
		/// Signals the OnTransferProgress event.
		/// </summary>
		/// <param name="e"></param>
		internal static void OnTransferProgress(TransferEventArgs e) {
			if(TransferProgress != null)
				TransferProgress(e);
		}

		/// <summary>
		/// Signals the OnTransferComplete event.
		/// </summary>
		/// <param name="e"></param>
		internal static void OnTransferComplete(TransferEventArgs e) {
			if(TransferComplete != null)
				TransferComplete(e);
		}
	}
}
