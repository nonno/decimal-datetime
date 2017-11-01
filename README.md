# Decimal DateTime

NuGet library to handle decimal time and republican (French) calendar.

It works like normal `DateTime`: it's possible to ask for the current DateTime (`DecimalDatetime.DecimalDatetime.Now`), the calculate the decimal date-time in a particular moment passing a DateTime (`new DecimalDatetime.DecimalDatetime(new DateTime())`).

It's possible to choose between different formats using the `ToString`.

## Decimal time

Decimal time was introduced in France a few years after the revolution of 1789, and for a while it replaced the normal way of measuring the time. With decimal time a day is divided in ten hours, each one made by 100 minutes and a minute is divided in 100 seconds. So a second is a bit faster because in a day there are 100000 seconds (10 x 100 x 100) instead of 86400 (24 x 60 x 60).

[More info about decimal time](https://en.wikipedia.org/wiki/Decimal_time).

## French republican calendar

To be fully revolutionary this watch converts also the current date in the French republican calendar, a calendar intruduced in France after the revolution. With this calendar every month is 30 days long, and every week 10 days long. In a year there are still 12 months, but there is also a much shorter 13th month used to "complete" the year.

[More info about republican calendar](https://en.wikipedia.org/wiki/French_Republican_Calendar).

## In memory of Alessandro Candelari, aka Pallettaro

This project was originally developed by Alessandro Candelari, a friend of mine who passed away in June 2017. For a while we helped him with some other friends, but as often happened with Ale, after some weeks of working he abandoned everything because he lost interest in it...
I truly believe that it's a pity to waste a so brilliant idea and quite good job, so I decided to keep the project alive.

![Pallettaro](readme/pallettaro.jpg)

## NuGet package

My NuGet package (.NET Standard 2.0) is this one: [Decimal.DateTime](https://www.nuget.org/packages/Decimal.DateTime/).

My friend's package: [DecimalDatetime.pallettaro](https://www.nuget.org/packages/DecimalDatetime.pallettaro/).

## Web API

A (small) set of API to get Revolutionary/Republican French calendar date and decimal time info.

### - api/datetime

Returns info about this moment:

```json
{"Timestamp":"225-05-02T04:96:07","GregorianTimestamp":"2017-01-21T11:54:21","Year":225,"Month":5,"MonthName":"Piovoso","Day":2,"DayName":"Muschio","Hour":4,"Minute":96,"Second":7,"DayInYear":122}
```

### - api/datetime/{string}

It tries to parse the {string} parameter returning info about that `DateTime` object, in the same format.
The parameter could be a date/time in every format, the parse is attempted in Italian, American English, British English and French.