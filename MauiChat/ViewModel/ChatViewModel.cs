using Syncfusion.Maui.Chat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiChat
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Current User of Chat
        /// </summary>
        private Author currentUser;

        /// <summary>
        /// Collection of messages in a conversation.
        /// </summary>
        private ObservableCollection<object> messages;

        #endregion

        #region Constructor
        public ChatViewModel()
        {
            this.messages = new ObservableCollection<object>();
            this.currentUser = new Author() { Name = "Nancy" };
            this.SendMessageCommand = new Command<object>(ExecuteSendMessageCommand);
            this.GenerateMessages();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current user of the message.
        /// </summary>
        public Author CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
                RaisePropertyChanged(nameof(CurrentUser));
            }
        }

        /// <summary>
        /// Gets or sets the collection of messages of a conversation.
        /// </summary>
        public ObservableCollection<object> Messages
        {
            get
            {
                return messages;
            }
            set
            {
                messages = value;
                RaisePropertyChanged(nameof(Messages));
            }
        }

        /// <summary>
        /// Gets or sets the command used to send a message in the chat.
        /// </summary>
        public ICommand SendMessageCommand { get; set; }

        #endregion

        /// <summary>
        /// Property changed handler.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #region Private Methods

        /// <summary>
        /// Occurs when property is changed.
        /// </summary>
        /// <param name="propertyName">changed property name</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Generates the messages and adds them to the messages collection.
        /// </summary>
        private async void GenerateMessages()
        {
            var initialMessage = new TextMessage
            {
                Author = currentUser,
                Text = "Hi guys, good morning! I'm very delighted to share with you the news that our team is going to launch a new mobile application.",
                DeliveryState = Syncfusion.Maui.Chat.DeliveryStates.Sent,
            };
            messages.Add(initialMessage);
            UpdateDeliveryStatesIfCurrentUser(initialMessage);

            var responses = new[]
            {
                new TextMessage
                {
                    Author = new Author { Name = "Andrea", Avatar = "peoplecircle16.png" },
                    Text = "Oh! That's great."
                },
                new TextMessage
                {
                    Author = new Author { Name = "Harrison", Avatar = "peoplecircle14.png" },
                    Text = "That is good news."
                }
            };

            foreach (var response in responses)
            {
                await Task.Delay(1000).ConfigureAwait(true);
                messages.Add(response);
            }
        }

        /// <summary>
        /// Handles the send message command by updating the delivery state of the sent message to <see cref="Syncfusion.Maui.Chat.DeliveryStates.Sent"/>.
        /// </summary>
        /// <param name="sender">
        /// The command sender, expected to be of type <see cref="SendMessageEventArgs"/> containing the message to update.
        /// </param>
        private void ExecuteSendMessageCommand(object sender)
        {
            if (sender is SendMessageEventArgs eventArgs && eventArgs.Message is TextMessage message)
            {
                message.DeliveryState = Syncfusion.Maui.Chat.DeliveryStates.Sent;
                UpdateDeliveryStatesIfCurrentUser(message);
            }
        }

        /// <summary>
        /// Asynchronously updates the delivery state of a message if the specified author is the current user.
        /// The delivery state transitions through Sent, Delivered, and Read, each after a delay,
        /// simulating the typical message delivery process in chat applications.
        /// </summary>
        /// <param name="messageObj">The message object to update. Must be of type <see cref="TextMessage"/>.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async void UpdateDeliveryStatesIfCurrentUser(TextMessage messageObj)
        {
            if (messageObj.Author == CurrentUser)
            {
                await Task.Delay(1000).ConfigureAwait(true);
                messageObj.DeliveryState = Syncfusion.Maui.Chat.DeliveryStates.Delivered;
                await Task.Delay(1000).ConfigureAwait(true);
                messageObj.DeliveryState = Syncfusion.Maui.Chat.DeliveryStates.Read;
            }
        }

        #endregion
    }
}
