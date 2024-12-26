using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    namespace WpfApp3
    {
        class Program
        {
            private static readonly object _lock = new object();
            private static int _sharedResource = 0;
            private static Mutex _mutex = new Mutex();
            private static Semaphore _semaphore = new Semaphore(2, 2); // Разрешаем 2 потока одновременно

            static void Main(string[] args)
            {
                // 2. Блокировка потока с помощью Lock и Monitor
                TestLockAndMonitor();

                // 3. Реализация мьютекса
                TestMutex();

                // 3. Реализация семафора
                TestSemaphore();

                // 4. Многопоточность с помощью Task
                TestTask();

                // 5. Многопоточный цикл с помощью Parallel.For
                TestParallelFor();

                // 6. Асинхронная реализация метода с помощью async, await
                TestAsyncAwait().Wait();
            }

            // 2. Блокировка потока с помощью Lock и Monitor
            static void TestLockAndMonitor()
            {
                Console.WriteLine("Тестирование Lock и Monitor.");

                Thread thread1 = new Thread(IncrementResourceWithLock);
                Thread thread2 = new Thread(IncrementResourceWithMonitor);

                thread1.Start();
                thread2.Start();

                thread1.Join();
                thread2.Join();

                Console.WriteLine($"Итоговое значение общего ресурса (Lock/Monitor): {_sharedResource}");
            }

            static void IncrementResourceWithLock()
            {
                for (int i = 0; i < 100000; i++)
                {
                    lock (_lock) // Используем lock для синхронизации
                    {
                        _sharedResource++;
                    }
                }
            }

            static void IncrementResourceWithMonitor()
            {
                for (int i = 0; i < 100000; i++)
                {
                    Monitor.Enter(_lock); // Используем Monitor для синхронизации
                    _sharedResource++;
                    Monitor.Exit(_lock);
                }
            }

            // 3. Реализация мьютекса
            static void TestMutex()
            {
                Console.WriteLine("Тестирование Mutex...");

                Thread thread1 = new Thread(IncrementResourceWithMutex);
                Thread thread2 = new Thread(IncrementResourceWithMutex);

                thread1.Start();
                thread2.Start();

                thread1.Join();
                thread2.Join();

                Console.WriteLine($"Итоговое значение общего ресурса (Mutex): {_sharedResource}");
            }

            static void IncrementResourceWithMutex()
            {
                for (int i = 0; i < 100000; i++)
                {
                    _mutex.WaitOne(); // Захватываем мьютекс
                    _sharedResource++;
                    _mutex.ReleaseMutex(); // Освобождаем мьютекс
                }
            }

            // 3. Реализация семафора
            static void TestSemaphore()
            {
                Console.WriteLine("Тестирование Semaphore...");

                Thread thread1 = new Thread(IncrementResourceWithSemaphore);
                Thread thread2 = new Thread(IncrementResourceWithSemaphore);
                Thread thread3 = new Thread(IncrementResourceWithSemaphore);

                thread1.Start();
                thread2.Start();
                thread3.Start();

                thread1.Join();
                thread2.Join();
                thread3.Join();

                Console.WriteLine($"Итоговое значение общего ресурса (Semaphore): {_sharedResource}");
            }

            static void IncrementResourceWithSemaphore()
            {
                for (int i = 0; i < 100000; i++)
                {
                    _semaphore.WaitOne(); // Захватываем семафор
                    _sharedResource++;
                    _semaphore.Release(); // Освобождаем семафор
                }
            }

            // 4. Многопоточность с помощью Task
            static void TestTask()
            {
                Console.WriteLine("Тестирование Task...");

                Task task1 = Task.Run(() => IncrementResourceWithTask());
                Task task2 = Task.Run(() => IncrementResourceWithTask());

                Task.WaitAll(task1, task2);

                Console.WriteLine($"Итоговое значение общего ресурса (Task): {_sharedResource}");
            }

            static void IncrementResourceWithTask()
            {
                for (int i = 0; i < 100000; i++)
                {
                    lock (_lock) // Используем lock для синхронизации
                    {
                        _sharedResource++;
                    }
                }
            }

            // 5. Многопоточный цикл с помощью Parallel.For
            static void TestParallelFor()
            {
                Console.WriteLine("Тестирование Parallel.For...");

                Parallel.For(0, 100000, i =>
                {
                    lock (_lock) // Используем lock для синхронизации
                    {
                        _sharedResource++;
                    }
                });

                Console.WriteLine($"Итоговое значение общего ресурса (Parallel.For): {_sharedResource}");
            }

            // 6. Асинхронная реализация метода с помощью async, await
            static async Task TestAsyncAwait()
            {
                Console.WriteLine("Тестирование async/await...");

                await Task.Run(() => IncrementResourceAsync());

                Console.WriteLine($"Итоговое значение общего ресурса (async/await): {_sharedResource}");
            }

            static async Task IncrementResourceAsync()
            {
                for (int i = 0; i < 100000; i++)
                {
                    await Task.Run(() =>
                    {
                        lock (_lock) // Используем lock для синхронизации
                        {
                            _sharedResource++;
                        }
                    });
                }
            }
        }
    }
}
