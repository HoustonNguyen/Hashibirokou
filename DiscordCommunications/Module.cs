
using Discord;
using Discord.Webhook;
using System;
using System.Collections.Generic;

namespace DiscordCommunications
{
    public class Module
    {
        public class MessageOptions
        {
            public string WebHookURL { get; set; }
            public string Text { get; set; }
            public bool IsTTS { get; set; } = false;
            public IEnumerable<Embed> Embeds { get; set; } 
            public string Username { get; set; } 
            public string AvatarUrl { get; set; } 
            public RequestOptions Options { get; set; } 
        }

        public static bool SendMessage(MessageOptions options)
        {
            DiscordWebhookClient discordWebhookClient = null;
            try
            {
                discordWebhookClient = new DiscordWebhookClient(options.WebHookURL);
            }
            catch (Exception ex)
            {
                throw new DiscordCommunications.Exceptions.DiscordClientInstantiationException("Failed to Instantiate DiscordWebhookClient", ex.InnerException);
            }

            try
            {
                ////But a channel has a 30 msg/60 sec limit for webhooks
                discordWebhookClient.SendMessageAsync(
                    text: options.Text, 
                    isTTS: options.IsTTS, 
                    embeds: options.Embeds, 
                    username: options.Username, 
                    avatarUrl: options.AvatarUrl, 
                    options: options.Options);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool SendMessage(string webHookURL, string messageText, string userName = null, string avatarUrl = null)
        {
            DiscordWebhookClient discordWebhookClient = null;
            try
            {
                discordWebhookClient = new DiscordWebhookClient(webHookURL);
            }
            catch (Exception ex)
            {
                throw new DiscordCommunications.Exceptions.DiscordClientInstantiationException("Failed to Instantiate DiscordWebhookClient", ex.InnerException);
            }

            try 
            {
                ////But a channel has a 30 msg/60 sec limit for webhooks
                discordWebhookClient.SendMessageAsync(
                            text: messageText,
                            username: userName,
                            avatarUrl: avatarUrl
                            );
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
