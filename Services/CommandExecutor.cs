﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrumBot.Commands;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DrumBot.Services
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly List<BaseCommand> _commands;
        private BaseCommand _lastCommand;

        public CommandExecutor(IServiceProvider serviceProvider)
        {
            _commands = serviceProvider.GetServices<BaseCommand>().ToList();
        }
        
        public async Task Execute(Update update)
        {
            if(update?.Message?.Chat == null && update?.CallbackQuery == null)
                return;

            if (update.Type == UpdateType.Message)
            {
                switch (update.Message?.Text)
                {
                    case "Начать заниматься":
                        await ExecuteCommand(CommandNames.AddJournalWriteCommand, update);
                        return;
                    case "Скачать книгу":
                        await ExecuteCommand(CommandNames.DownloadBookCommand, update);
                        return;
                }
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                if (update.CallbackQuery.Data.Contains("download"))
                {
                    await ExecuteCommand(CommandNames.DownloadBookCommand, update);
                    return;
                }
            }
            
            if (update.Message != null && update.Message.Text.Contains(CommandNames.StartCommand))
            {
                await ExecuteCommand(CommandNames.StartCommand, update);
                return;
            }

            // AddOperation => SelectCategory => FinishOperation
            switch (_lastCommand?.Name)
            {
                case CommandNames.AddOperationCommand:
                {
                    await ExecuteCommand(CommandNames.SelectCategoryCommand, update);
                    break;
                }
                case CommandNames.SelectCategoryCommand:
                {
                    await ExecuteCommand(CommandNames.FinishOperationCommand, update);
                    break;
                }
                case null:
                {
                    await ExecuteCommand(CommandNames.StartCommand, update);
                    break;
                }
            }
        }

        private async Task ExecuteCommand(string commandName, Update update)
        {
            _lastCommand = _commands.First(x => x.Name == commandName);
            await _lastCommand.ExecuteAsync(update);
        }
    }
}