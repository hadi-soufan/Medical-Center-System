export default class CustomFileSystemProvider {
    constructor(files) {
      this.files = files;
    }
  
    getItems(path) {
      // Ensure files is defined and has $values array
      if (!this.files || !this.files.$values) {
        return Promise.resolve([]);
      }
      
      // Map over the $values array to create file items in the required format
      return Promise.resolve(this.files.$values.map(file => ({
        name: this.extractFileName(file.url),  // Extract file name from URL
        isDirectory: false,
        size: file.size || 0, // Assuming size is important, otherwise you can remove this or set a default
        thumbnail: file.url,
        dateModified: file.dateModified || new Date().toISOString(), // Use current date if not provided
        key: file.id  // Add unique key for identification (optional, but often helpful)
      })));
    }

    // Helper method to extract the file name from a URL
    extractFileName(url) {
        return url.split('/').pop();
    }
}
