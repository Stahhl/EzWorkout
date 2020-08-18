using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzWorkout.Models;
using EzWorkout.Services;
using SQLite;

namespace EzWorkout.Database
{
    public class WorkoutDb
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        //Init
        public WorkoutDb()
        {
            InitializeAsync().SafeFireAndForget(false);
            //InitializeAsync();
        }
        public async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(_Interval).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(_Interval)).ConfigureAwait(false);
                }
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Workout).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Workout)).ConfigureAwait(false);
                }

                //await SeedData();

                initialized = true;
            }
        }

        public async Task SeedData()
        {
            throw new NotImplementedException("temp");
            //if (await GetNumberOfItems() == 1)
            //    return;



            //var list = new List<_Interval>();
            //list.Add(new DistanceInterval(IntervalIntensity.INACTIVE, 15));
            //list.Add(new DurationInterval(IntervalIntensity.MEDIUM, TimeSpan.FromSeconds(3)));
            //list.Add(new GoToInterval(0, 1));
            //list.Add(new DurationInterval(IntervalIntensity.HIGH, TimeSpan.FromSeconds(3)));


            //var x = await SaveItemAsync(new Workout(list) { Name = "Workout 1" });
        }

        public async Task<int> GetNumberOfItems()
        {
            return await Database.Table<Workout>().CountAsync();
        }
        //Data manipulation methods
        public async Task<List<Workout>> GetItemsAsync()
        {
            var workouts = await Database.Table<Workout>().ToListAsync();
            var intervals = await Database.Table<_Interval>().ToListAsync();

            foreach (var workout in workouts)
            {
                workout.Intervals = intervals.Where(x => x.WorkoutId == workout.Id).ToList();
            }

            return workouts;
        }
        public Task<Workout> GetItemAsync(int id)
        {
            return Database.Table<Workout>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveItemAsync(Workout item)
        {
            if (item.Id != 0)
            {
                await Database.UpdateAsync(item);
            }
            else
            {
                await Database.InsertAsync(item);
            }

            //save intervals after workout, intervals have a workoutId FK
            foreach (var interval in item.Intervals)
            {
                interval.WorkoutId = item.Id;

                await SaveItemAsync(interval);
            }

            return item.Id;
        }
        private Task<int> SaveItemAsync(_Interval item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item, typeof(_Interval));
            }
            else
            {
                return Database.InsertAsync(item, typeof(_Interval));
            }
        }
        public Task<int> DeleteItemAsync(Workout item)
        {
            foreach (var interval in item.Intervals)
            {
                DeleteItemAsync(interval);
            }

            return Database.DeleteAsync(item);
        }
        private Task<int> DeleteItemAsync(_Interval item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
