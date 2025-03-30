function openDB(dbName, storeName, dbVersion) {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(dbName, dbVersion);

        request.onerror = () => reject(request.error);
        request.onsuccess = () => resolve(request.result);

        request.onupgradeneeded = () => {
            const db = request.result;
            if (!db.objectStoreNames.contains(storeName)) {
                db.createObjectStore(storeName);
            }
        };
    });
}

function loadData(key, dbName, storeName) {
    return openDB(dbName, storeName).then(db => {
        return new Promise((resolve, reject) => {
            const transaction = db.transaction(storeName, 'readonly');
            const store = transaction.objectStore(storeName);
            const query = store.get(key);

            query.onsuccess = () => resolve(query.result);
            query.onerror = () => reject(query.error);
        });
    }).catch(error => Promise.reject(error));
}

function saveData(key, data, dbName, storeName) {
    return openDB(dbName, storeName).then(db => {
        return new Promise((resolve, reject) => {
            const transaction = db.transaction(storeName, 'readwrite');
            const store = transaction.objectStore(storeName);
            const addRequest = store.put(data, key);

            addRequest.onsuccess = () => resolve();
            addRequest.onerror = () => reject(addRequest.error);
        });
    }).catch(error => Promise.reject(error));
}

function removeData(key, dbName, storeName) {
    return openDB(dbName, storeName).then(db => {
        return new Promise((resolve, reject) => {
            const transaction = db.transaction(storeName, 'readwrite');
            const store = transaction.objectStore(storeName);
            const deleteRequest = store.delete(key);

            deleteRequest.onsuccess = () => resolve();
            deleteRequest.onerror = () => reject(deleteRequest.error);
        });
    }).catch(error => Promise.reject(error));
}