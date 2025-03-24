
## 🧠 **Asset Tracker — App Logic Overview**

---

### 🟡 **1. Program.cs (Entry Point)**

- Kicks off the app by:
  - Fetching **currency exchange rates** from the ECB
  - Creating three **offices** (`USA`, `Sweden`, `Germany`)
  - Initializing the **AssetTrackerService**
  - Loading **sample assets** via `SeedData`
  - Starting the interactive **console menu** via `Menu.Start()`

---

### 📦 **2. SeedData.cs**

- Adds a predefined list of `Computer` and `Smartphone` objects to the tracker.
- Each asset has:
  - A **brand**
  - A **model**
  - A **purchase date**
  - A **price** (amount + currency)
  - A linked **office**

---

### 🧰 **3. AssetTrackerService.cs**

- Acts as your in-memory "database" of assets.
- Can:
  - Add new assets
  - Return all assets
  - Return sorted lists by:
    - Type & Purchase Date
    - Office & Purchase Date

---

### 🖥️ **4. Menu.cs (User Interface)**

- Console-based menu with options to:
  1. Show all assets sorted by **Type + Date**
  2. Show all assets sorted by **Office + Date**
  3. Add a new asset manually
  0. Exit
- Calls `Formatter` to print assets with the correct formatting.

---

### 🎨 **5. Formatter.cs**

- Prints each asset with color-coding based on **age**:
  - **RED** if less than **3 months** from 3-year lifespan
  - **YELLOW** if less than **6 months**
  - Normal otherwise
- Also:
  - Converts each asset's price into its office's **local currency** using live ECB rates.
  - Formats the data into clean columns for the console.

---

### 💱 **6. CurrencyConverter.cs + ECBEnvelope.cs**

- Downloads **live exchange rates** from the European Central Bank.
- Deserializes the XML into objects using `ECBEnvelope.cs`.
- Can convert:
  - From **EUR → target currency**
  - Used in `Formatter` to show localized pricing

---

### 📦 **7. Models (Asset, Computer, Smartphone, etc.)**

- `Asset` is the base class for both `Computer` and `Smartphone`
- Each asset contains:
  - `Brand`, `Model`
  - `PurchaseDate`, `Price`, `Office`
  - `Type` (string)

---

### ⚙️ Bonus: How Age Is Calculated

- `Formatter` figures out how many **months ago** an asset was purchased
- Compares that to the lifespan (36 months)
- If it's:
  - ≤ 3 months left → 🔴 RED
  - ≤ 6 months left → 🟡 YELLOW
  - Otherwise → normal

---

## 🎯 Final Result:
You've built a fully working, extendable **console app** that:
- Tracks assets by brand, model, and office
- Highlights upcoming replacements
- Converts prices into local currencies
- Supports dynamic data input
- Uses **clean architecture** with separation of concerns

---
