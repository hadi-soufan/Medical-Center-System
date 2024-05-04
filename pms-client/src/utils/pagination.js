export class Pagination {
    constructor(currentPage, itemsPerPage, totalItems, totalPages) {
        this.currentPage = currentPage;
        this.itemsPerPage = itemsPerPage;
        this.totalItems = totalItems;
        this.totalPages = totalPages;
    }
}

export class PaginatedResult {
    constructor(data, pagination) {
        this.data = data;
        this.pagination = pagination;
    }
}

export class PagingParams {
    pageNumber = 1;
    pageSize = 2;
}