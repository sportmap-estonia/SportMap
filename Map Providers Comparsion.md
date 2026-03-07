
This allows us to find places within a given distance of a point.

Google’s Places Nearby API also supports radius queries, but once we store places in our own database we can avoid external API costs.

## Sorting

Sorting by **distance** is straightforward using geographic distance.

Sorting by **price** requires the attribute to exist in our dataset (for example entry fee or membership cost).

Sorting by **popularity** is more difficult because Google’s ranking uses internal engagement signals that we cannot access.

For our MVP we could sort using:

- distance (default)
- average user rating
- number of check-ins or visits
- number of saves/favorites

## Popular Times Feature

Google’s busy-time charts only exist within their UI.

If we want similar functionality, we would need to collect our own usage data such as:

- user check-ins
- visit timestamps
- aggregated activity trends

Another option would be integrating third-party mobility analytics services, although this typically involves additional cost and complexity.

For an MVP, implementing this feature may not be necessary initially.

---

# Caching & Performance

## Tile Caching

Except for Google (which disallows caching), we can cache map tiles.

Examples include:

- using a CDN in front of Mapbox or HERE tiles
- generating and hosting our own tile sets
- storing offline packages where supported

## POI Query Caching

To reduce costs on external APIs, we can cache expensive requests such as geocoding or radius queries.

If we store place data in **PostGIS**, we can serve most queries directly from our own database, eliminating additional API costs.

---

# Trade-offs & Recommendation

## Map Styling & POIs

We prefer **Mapbox** for custom styling because it allows easy integration with custom data layers from PostGIS and provides strong vector tile styling capabilities.

This is particularly useful for drawing our own POIs with custom icons and colors.

Google Maps supports markers but offers less flexibility in styling.

## Free Usage

For MVP-level traffic:

- Mapbox (50k loads/month) is acceptable
- HERE offers a larger free allowance (250k transactions)
- Google provides about 100k loads through its monthly credit

ArcGIS offers large free quotas but involves deeper integration with the Esri ecosystem.

OSM has no direct usage cost but requires us to manage tile hosting.

## Popularity Data

If “busy times” becomes a core feature, we should note that **no major provider offers this data for free**.

Google internally compiles it but does not expose it.

If we want a similar feature we would need:

- crowd-sourced user activity data
- external analytics datasets
- or our own engagement metrics

For the MVP, simpler ranking such as **distance or user ratings** is likely sufficient.

## Developer Experience

All major SDKs work with React or Next.js:

- Mapbox GL JS
- Google Maps JS
- HERE Maps JS
- ArcGIS JS

However, Mapbox and Google currently have the strongest community support and documentation.

---

# Conclusion

For the SportMap MVP, **Mapbox appears to be the strongest candidate for the map layer** due to:

- strong custom styling
- good developer experience
- acceptable free usage tier
- easy integration with our own POI data

We can store and query places in **PostGIS**, which allows us to handle geospatial search (such as radius queries) without relying heavily on external APIs.

For popularity-based ranking, we should initially rely on **user-generated metrics** such as ratings or check-ins, since Google’s “popular times” data is not publicly accessible.

If necessary, we can evaluate third-party datasets or build our own activity analytics later as the platform grows.
