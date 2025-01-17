import sqlite3
from functools import cache
from aiogram import Bot,types
from aiogram.dispatcher import Dispatcher, FSMContext
from aiogram.dispatcher.filters.state import StatesGroup, State
from aiogram.types import callback_query, message, reply_keyboard,ParseMode
from aiogram.utils import executor

import random
import anseiUserReg
import os
import os.path

TOKEN = "6109283978:AAHnZAcgDjowE1nw6d7F9bj_4XIP4vdiwuI"
bot = Bot(TOKEN)
dp = Dispatcher(bot)
db = 'K:\ANSEI_PROGRAMMS\ANSEI_BOT\ANSEIAPPS\ANSEIAPPS\AnseiBuyersDB.db'
userLogin = ""
userPassword = ""
conn = sqlite3.connect("AnseiBuyersDB.db")
cursor = conn.cursor()

@dp.message_handler()
async def command_func(message : types.Message):
    if message.text == "/start":
       buttons = [
           types.InlineKeyboardButton(text="РЕГИСТРАЦИЯ", callback_data="REG"),
           types.InlineKeyboardButton(text="АВТОРИЗАЦИЯ", callback_data="AUTH"),
           types.InlineKeyboardButton(text="ПОДДЕРЖКА",callback_data="SUP"),
       ]

       keyboard = types.InlineKeyboardMarkup(row_width=2)
       keyboard.add(*buttons)

       await message.answer('Привет!',reply_markup=keyboard)



@dp.callback_query_handler(text_contains = "AUTH")
async def command_func(call : callback_query.CallbackQuery):
    await bot.delete_message(chat_id=call.from_user.id, message_id=call.message.message_id)
    buttons2 = [
        types.InlineKeyboardButton(text="ПЕРЕЙТИ", url="https://t.me/ansei_apps"),
        types.InlineKeyboardButton(text="НАЗАД",callback_data="BACK"),
    ]
    keyboard2 = types.InlineKeyboardMarkup(row_width=3)
    keyboard2.add(*buttons2)
    await call.answer(cache_time = 60)
    await call.message.answer('Примеры в нашем канале',reply_markup = keyboard2)

@dp.callback_query_handler(text_contains = "PRIMER")
async def command_func(call : callback_query.CallbackQuery):
    await bot.delete_message(chat_id=call.from_user.id, message_id=call.message.message_id)
    buttons2 = [
        types.InlineKeyboardButton(text="ПЕРЕЙТИ", url="https://t.me/ansei_apps"),
        types.InlineKeyboardButton(text="НАЗАД",callback_data="BACK"),
    ]
    keyboard2 = types.InlineKeyboardMarkup(row_width=3)
    keyboard2.add(*buttons2)
    await call.answer(cache_time = 60)
    await call.message.answer('Примеры в нашем канале',reply_markup = keyboard2)

@dp.callback_query_handler(text_contains = "SUP")
async def command_func(call : callback_query.CallbackQuery):
    await bot.delete_message(chat_id=call.from_user.id, message_id=call.message.message_id)
    buttons2 = [
        types.InlineKeyboardButton(text="ПЕРЕЙТИ", url='https://t.me/ansei_group_of_games'),
        types.InlineKeyboardButton(text="НАЗАД",callback_data="BACK"),
    ]
    keyboard2 = types.InlineKeyboardMarkup(row_width=1)
    keyboard2.add(*buttons2)
    await call.answer(cache_time = 60)
    await call.message.answer('НАЖМИТЕ ЧТОБЫ ПЕРЕЙТИ В ЧАТ С ПОДДЕРЖКОЙ',reply_markup = keyboard2)

@dp.callback_query_handler(text_contains = "SAP")
async def command_func(call : callback_query.CallbackQuery):
    await bot.delete_message(chat_id=call.from_user.id, message_id=call.message.message_id)
    buttons2 = [
        types.InlineKeyboardButton(text="ПЕРЕЙТИ", url='https://t.me/ansei_group_of_games'),
        types.InlineKeyboardButton(text="НАЗАД",callback_data="BACK"),
    ]
    keyboard2 = types.InlineKeyboardMarkup(row_width=1)
    keyboard2.add(*buttons2)
    await call.answer(cache_time = 60)
    await call.message.answer('НАЖМИТЕ ЧТОБЫ ПЕРЕЙТИ В ЧАТ С ПОДДЕРЖКОЙ',reply_markup = keyboard2)

@dp.callback_query_handler(text_contains = "BACK")
async def command_func(call : callback_query.CallbackQuery):
    await bot.delete_message(chat_id=call.from_user.id, message_id=call.message.message_id)
    await call.answer(cache_time = 60)
    buttons3 = [
        types.InlineKeyboardButton(text="ПОДДЕРЖКА", callback_data="SUP"),
        types.InlineKeyboardButton(text="ПРИМЕРЫ ПРИЛОЖЕНИИ", callback_data="PRIMER"),
        types.InlineKeyboardButton(text="ЗАКАЗАТЬ", callback_data="SAP")
    ]

    keyboard3 = types.InlineKeyboardMarkup(row_width=2)
    keyboard3.add(*buttons3)

    await call.message.answer(
        'Нажмите на нужную кнопку',
        reply_markup=keyboard3)

@dp.callback_query_handler(text_contains = "REG")
async def command_func(call : callback_query.CallbackQuery):
    await bot.delete_message(chat_id=call.from_user.id, message_id=call.message.message_id)
    await call.answer(cache_time=60)
    msg = await call.answer(call.message.from_user.id,'Введите Логин, Пароль')
    start_2(msg)

def start_2(message):
    first = message.text.split()[0]
    last = message.text.split()[1]
    create_user_db(first, last)

def create_user_db(first, last):
    cursor.execute(f"INSERT INTO users VALUES (?, ?,?,?,?)", (first,last,str(random.randint(1000,100000)),0,str(0)))
    conn.commit()

executor.start_polling(dp,skip_updates = True)