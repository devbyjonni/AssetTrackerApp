## **Asset Tracker — App Logic Overview**

---

### **1. Program.cs (Entry Point)**

- Kicks off the app by:
  - Fetching **currency exchange rates** from the ECB
  - Creating three **offices** (`USA`, `Sweden`, `Germany`)
  - Initializing the **AssetTrackerService**
  - Loading **sample assets** via `SeedData`
  - Starting the interactive **console menu** via `Menu.Start()`

---

### **2. SeedData.cs**

- Adds a predefined list of `Computer` and `Smartphone` objects to the tracker.
- Each asset has:
  - A **brand**
  - A **model**
  - A **purchase date**
  - A **price** (amount + currency)
  - A linked **office**

---

### **3. AssetTrackerService.cs**

- Acts as your in-memory "database" of assets.
- Can:
  - Add new assets
  - Return all assets
  - Return sorted lists by:
    - Type & Purchase Date
    - Office & Purchase Date

---

###  **4. Menu.cs (User Interface)**

- Console-based menu with options to:
  1. Show all assets sorted by **Type + Date**
  2. Show all assets sorted by **Office + Date**
  3. Add a new asset manually
  0. Exit
- Calls `Formatter` to print assets with the correct formatting.

---

### **5. Formatter.cs**

- Prints each asset with color-coding based on **age**:
  - **RED** if less than **3 months** from 3-year lifespan
  - **YELLOW** if less than **6 months**
  - Normal otherwise
- Also:
  - Converts each asset's price into its office's **local currency** using live ECB rates.
  - Formats the data into clean columns for the console.

---

### **6. CurrencyConverter.cs + ECBEnvelope.cs**

- Downloads **live exchange rates** from the European Central Bank.
- Deserializes the XML into objects using `ECBEnvelope.cs`.
- Can convert:
  - From **EUR → target currency**
  - Used in `Formatter` to show localized pricing

---

### **7. Models (Asset, Computer, Smartphone, etc.)**

- `Asset` is the base class for both `Computer` and `Smartphone`
- Each asset contains:
  - `Brand`, `Model`
  - `PurchaseDate`, `Price`, `Office`
  - `Type` (string)

---

### Bonus: How Age Is Calculated

- `Formatter` figures out how many **months ago** an asset was purchased
- Compares that to the lifespan (36 months)
- If it's:
  - ≤ 3 months left → 🔴 RED
  - ≤ 6 months left → 🟡 YELLOW
  - Otherwise → normal

---

## Final Result:
You've built a fully working, extendable **console app** that:
- Tracks assets by brand, model, and office
- Highlights upcoming replacements
- Converts prices into local currencies
- Supports dynamic data input
- Uses **clean architecture** with separation of concerns

---
 Below is a concise summary of the logic and flow of the application:

⸻

### High-Level Overview

1. **Domain Models & OOP Structure:**
   - **Asset (abstract)**: 
     - Serves as the base class for different asset types.
     - Contains common properties such as Brand, Model, Purchase Date, Price, and Office.
   - **Computer & Smartphone**:
     - Inherit from Asset and set their respective type (“Computer” or “Smartphone”) in the constructor.
   - **Office & Price**:
     - Represent the location details and monetary information for assets.
   - **Currency Enum**:
     - Defines supported currencies (USD, EUR, SEK).

2. **Data Layer:**
   - **AssetRepository**:
     - Functions as an in-memory store for assets.
     - Provides methods to add assets and retrieve sorted views (by type & purchase date, or office & purchase date).
   - **SeedData**:
     - Populates the repository with initial sample assets.

3. **Services:**
   - **CurrencyConverter**:
     - Fetches and parses exchange rates from the European Central Bank (ECB) in XML.
     - Converts asset prices from EUR to the office’s local currency.
     - Uses robust error handling and culture-invariant parsing to ensure correct conversions.

4. **Console UI:**
   - **Menu**:
     - The entry point for user interaction.
     - Displays a main menu with options to:
       - View assets sorted by type & purchase date.
       - View assets sorted by office & purchase date.
       - Create a new asset.
       - Exit the application.
     - Uses dependency injection for input/output (via TextReader/TextWriter) so that it’s testable.
   - **ConsoleFormatter**:
     - Handles the presentation of asset details.
     - Formats output into a neat table with column headers.
     - Applies color coding (or color markers) based on asset age (to signal when an asset is nearing the end of its lifespan).

5. **Application Flow (Program.cs):**
   - **Initialization**:
     - Currency rates are fetched via `CurrencyConverter.UpdateRates()` (with error handling).
     - Default office instances are created.
     - The AssetRepository is initialized and seeded with sample data.
   - **User Interaction**:
     - The Menu is started, displaying options to the user.
     - Based on user input, the menu will either display sorted asset lists (with headers) or trigger asset creation.
     - The design leverages injected input/output streams, making the application testable as well as interactive.

6. **Testing & Maintainability:**
   - The application is built with testability in mind:
     - The Menu’s methods accept custom input/output, allowing for simulated user interactions in unit tests.
     - Unit tests verify that the asset creation flow, menu navigation, and sorting functions work correctly.
     - Centralizing formatting logic (via `AssetTableHeader` and `ConsoleFormatter`) ensures consistency and ease of future modifications.

⸻

### Flow Summary

1. **Startup:**
   - The application initializes by fetching currency data, creating default offices, and seeding assets.
2. **Main Menu:**
   - The user is presented with a menu of options (view assets sorted by type or office, add a new asset, or exit).
3. **User Action:**
   - Depending on the user’s choice:
     - **View Options**:
       - The menu retrieves the sorted assets from the repository.
       - It prints a header row (defined in a centralized helper) followed by each asset’s formatted details using `ConsoleFormatter`.
     - **Asset Creation**:
       - The user is guided through input prompts.
       - Data is validated and parsed (using invariant culture and specific date formats).
       - A new asset is created (as a Computer or Smartphone) and added to the repository.
4. **Testability & Maintenance:**
   - The use of injected input/output in the Menu enables automated tests, ensuring each feature works as expected.
   - The centralized formatting and header definitions make updates easier and keep the code DRY.

This structure demonstrates strong object-oriented principles, a clear separation of concerns, and robust testability—all of which make the codebase maintainable and ready for future enhancements.

⸻

### Commands:

You can easily run your tests from the terminal and see their names and outcomes using the `dotnet test` command. For example:

```bash
dotnet test --logger:"console;verbosity=detailed"
dotnet run