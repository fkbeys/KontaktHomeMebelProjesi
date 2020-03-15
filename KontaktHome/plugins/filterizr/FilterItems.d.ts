//import { Filter } from '../filterizr/ActiveFilter';
import { Filter } from '../filterizr/types';
import FilterItem from '../../plugins/filterizr/FilterItem';
import FilterizrOptions from '../../plugins/filterizr/FilterizrOptions/FilterizrOptions';
export default class FilterItems {
    private filterItems;
    private options;
    constructor(filterItems: FilterItem[], options: FilterizrOptions);
    readonly length: number;
    get(): FilterItem[];
    getItem(index: number): FilterItem;
    set(filterItems: FilterItem[]): void;
    destroy(): void;
    updateTransitionStyle(): void;
    updateDimensions(): void;
    push(filterItem: FilterItem): number;
    getFiltered(filter: Filter): FilterItem[];
    getFilteredOut(filter: Filter): FilterItem[];
    getSorted(sortAttr?: string, sortOrder?: 'asc' | 'desc'): FilterItem[];
    getSearched(searchTerm: string): FilterItem[];
    getShuffled(): FilterItem[];
    private shouldBeFiltered;
}
