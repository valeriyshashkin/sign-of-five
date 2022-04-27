mergeInto(LibraryManager.library, {
  LocalStorageSetItem(key, value) {
    localStorage.setItem(key, value);
  },

  LocalStorageGetItem(key) {
    return localStorage.getItem(key);
  },
});
