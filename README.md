# message-delivery-states-.net-maui-chat

## Sample

```xaml

    <sfChat:SfChat x:Name="sfChat"
            Messages="{Binding Messages}"
            CurrentUser="{Binding CurrentUser}"
            SendMessageCommand="{Binding SendMessageCommand}"
            ShowDeliveryState="True"/>

ViewModel:

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

    private void ExecuteSendMessageCommand(object sender)
    {
        if (sender is SendMessageEventArgs eventArgs && eventArgs.Message is TextMessage message)
        {
            message.DeliveryState = Syncfusion.Maui.Chat.DeliveryStates.Sent;
            UpdateDeliveryStatesIfCurrentUser(message);
        }
    }

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

```

## Requirements to run the demo

To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements)

## Troubleshooting:
### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

## License

Syncfusion® has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion® liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion®'s samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion®'s samples.