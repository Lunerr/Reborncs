using Discord;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Text.RegularExpressions;

namespace Reborn.Common
{
    public struct InclusionCmd
    {
        public string response;
        public string path;
    }
    public static class Config
    {
        // Prefix
        public const char PREFIX = '!';

        // Directories
        public const string LOGS_DIRECTORY = "logs/";
        public const string DATA_DIRECTORY = "data/";
        public static IReadOnlyDictionary<string, InclusionCmd> INCLUSION_COMMANDS = new Dictionary<string, InclusionCmd>()
        {
            { "nigger", new InclusionCmd { path = "arnold.png" } },
            { "anal", new InclusionCmd { response = "Did someone say Vanalk? Fun fact: the word Vanalk has anal in the middle of it. If you do not believe me, please refer to the anal manual as soon as possible." } }
        }.ToImmutableDictionary();

        // Discord code responses
        public static readonly IReadOnlyDictionary<int, string> DISCORD_CODES = new Dictionary<int, string>()
        {
            { 20001, "Only a user account may perform this action." },
            { 50007, "I cannot DM you. Please allow direct messages from guild users." },
            { 50013, "I do not have permission to do that." },
            { 50034, "Discord does not allow bulk deletion of messages that are more than two weeks old." }
        }.ToImmutableDictionary();

        // HTTP code responses
        public static readonly IReadOnlyDictionary<HttpStatusCode, string> HTTP_CODES =
            new Dictionary<HttpStatusCode, string>()
            {
                { HttpStatusCode.Forbidden, "I do not have permission to do that." },
                { HttpStatusCode.InternalServerError, "An unexpected error has occurred, please try again later." },
                { HttpStatusCode.RequestTimeout, "The request has timed out, please try again later." }
            }.ToImmutableDictionary();

        // Regexes
        public static readonly Regex NEW_LINE_REGEX = new Regex(@"\r\n?|\n"), NUMBER_REGEX = new Regex(@"^\d+(\.\d+)?"),
            EMOTE_REGEX = new Regex(@"<:.+:\d+>"), MENTION_REGEX = new Regex(@"@here|@everyone|<@!?\d+>"),
            EMOTE_ID_REGEX = new Regex(@"<:.+:|>"), CAMEL_CASE = new Regex("(\\B[A-Z])"),
            MARKDOWN_REGEX = new Regex(@"\*|`|_|~");

        // Custom colors
        public static readonly Color ERROR_COLOR = new Color(0xFF0000), MUTE_COLOR = new Color(0xFF3E29),
            UNMUTE_COLOR = new Color(0x72FF65), CLEAR_COLOR = new Color(0x4D3DFF);

        // Default colors
        public static readonly IReadOnlyList<Color> DEFAULT_COLORS = new Color[]
        {
            new Color(0xFF269A), new Color(0x66FFCC),
            new Color(0x00FF00), new Color(0xB10DC9),
            new Color(0x00E828), new Color(0xFFFF00),
            new Color(0x08F8FF), new Color(0x03FFAB),
            new Color(0xF226FF), new Color(0xFF00BB),
            new Color(0xFF1C8E), new Color(0x00FFFF),
            new Color(0x68FF22), new Color(0x14DEA0),
            new Color(0xFFBE11), new Color(0x0FFFFF),
            new Color(0x2954FF), new Color(0x40E0D0),
            new Color(0x9624ED), new Color(0x01ADB0),
            new Color(0xA8ED00), new Color(0xBF255F)
        }.ToImmutableArray();

        // JSON serialization settings
        public static readonly JsonSerializerSettings JSON_SETTINGS = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Error
        };
    }
}
