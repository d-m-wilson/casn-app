import { InjectionToken } from '@angular/core';
import { environment } from '../../environments/environment';

export const BASE_PATH = environment.apiUrl;
export const COLLECTION_FORMATS = {
    'csv': ',',
    'tsv': '   ',
    'ssv': ' ',
    'pipes': '|'
}
