using System;

namespace Logic.Configuration.MappingProfiles
{
    public static class MappingAssembly
    {
        public static Type[] GetMappingProfileTypes()
            => new Type[]
        {
            typeof(UserProfile),
            typeof(SushiProfile),
            typeof(BasketProfile),
            typeof(SessionStateProfile)
        };
    }
}