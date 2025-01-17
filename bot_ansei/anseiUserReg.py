from functools import cache
from aiogram import Bot,types
from aiogram.dispatcher import Dispatcher, FSMContext
from aiogram.dispatcher.filters.state import StatesGroup, State
from aiogram.types import callback_query, message, reply_keyboard,ParseMode
from aiogram.utils import executor

import os
TOKEN = "6109283978:AAHnZAcgDjowE1nw6d7F9bj_4XIP4vdiwuI"
bot = Bot(TOKEN)
dp = Dispatcher(bot)

class AwaitMessages(StatesGroup):
    fio_add = State()
    phone_add = State()


@dp.message_handler(state=AwaitMessages.fio_add)
async def process_fio_add(message: types.Message, state: FSMContext):
    async with state.proxy() as data:
        data['fio'] = message.text
    await message.answer('Введите телефон.')
    await AwaitMessages.phone_add.set()


@dp.message_handler(state=AwaitMessages.phone_add)
async def process_fio_add(message: types.Message, state: FSMContext):
    async with state.proxy() as data:
        data['phone'] = message.text
        await message.answer(f'ФИО - {data["fio"]}\nНомер - {data["phone"]}')