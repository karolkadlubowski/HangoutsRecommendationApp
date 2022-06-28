export interface LocationCoordinate {
    locationCoordinateId: number;
    locationId: number;
    latitude: number;
    longitude: number;
}

export interface Location {
    locationId: number;
    venueId: number;
    address: string;
    locationCoordinate: LocationCoordinate;
}

export interface VenueResponse {
    venueId: number;
    name: string;
    description: string;
    categoryId: string;
    creatorId: number;
    status: number;
    location: Location;
}

export interface FavouritesResponse {
    venueId: number;
    userId: number;
    name: string;
    description: string;
    categoryName: string;
    creatorId: number;
}

export interface LocationCoordinate {
    locationCoordinateId: number;
    locationId: number;
    latitude: number;
    longitude: number;
}

export interface Location {
    locationId: number;
    venueId: number;
    address: string;
    locationCoordinate: LocationCoordinate;
}

export interface Photo {
    fileId: string;
    key: string;
    name: string;
    folderKey: string;
    fileUrl: string;
}

export interface VenueDetailsResponse {
    venueId: number;
    name: string;
    description: string;
    categoryId: string;
    creatorId: number;
    status: number;
    location: Location;
    photos: Photo[];
}
