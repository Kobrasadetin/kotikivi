using System;
using System.Linq;
using Interactions;
using Resources;

namespace PowerLines
{
    public static class PowerRuneMixer
    {
        public static ResourceType GetResourceType(PowerRuneType a)
        {
            switch (a)
            {
                default:
                case PowerRuneType.RED:
                    return ResourceType.WARMTH;
                case PowerRuneType.BLUE:
                    return ResourceType.WATER;
                case PowerRuneType.GREEN:
                    return ResourceType.NUTRIENT;
                case PowerRuneType.WHITE:
                    return ResourceType.LIGTH;
            }
        }

        public static InterActionType GetMix2(PowerRuneType[] runes)
        {
            if (runes.Length == 2)
            {
                if (runes.Contains(PowerRuneType.RED) && runes.Contains(PowerRuneType.BLUE))
                {
                    return InterActionType.FUNGI;
                }
                if (runes.Contains(PowerRuneType.RED) && runes.Contains(PowerRuneType.GREEN))
                {
                    return InterActionType.FIRE;
                }
                if (runes.Contains(PowerRuneType.RED) && runes.Contains(PowerRuneType.WHITE))
                {
                    return InterActionType.SUN;
                }
                if (runes.Contains(PowerRuneType.BLUE) && runes.Contains(PowerRuneType.GREEN))
                {
                    return InterActionType.GRASS;
                }
                if (runes.Contains(PowerRuneType.BLUE) && runes.Contains(PowerRuneType.WHITE))
                {
                    return InterActionType.RAIN;
                }
                if (runes.Contains(PowerRuneType.GREEN) && runes.Contains(PowerRuneType.WHITE))
                {
                    return InterActionType.WIND;
                }
            }
            else if (runes.Length == 3)
            {
                if (runes.Contains(PowerRuneType.RED) && runes.Contains(PowerRuneType.BLUE) &&
                    runes.Contains(PowerRuneType.GREEN))
                {
                    return InterActionType.INSECTS;
                }
                if (runes.Contains(PowerRuneType.RED) && runes.Contains(PowerRuneType.BLUE) &&
                    runes.Contains(PowerRuneType.WHITE))
                {
                    return InterActionType.LEAFY_GREENS;
                }
                if (runes.Contains(PowerRuneType.RED) && runes.Contains(PowerRuneType.GREEN) &&
                    runes.Contains(PowerRuneType.WHITE))
                {
                    return InterActionType.BUSHES;
                }
                if (runes.Contains(PowerRuneType.BLUE) && runes.Contains(PowerRuneType.GREEN) &&
                    runes.Contains(PowerRuneType.WHITE))
                {
                    return InterActionType.CONIFERS;
                }
            }

            throw new ArgumentOutOfRangeException("Invalid combination!");
        }

    }
}