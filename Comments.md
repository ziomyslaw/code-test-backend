# Areas of concern:
1. ProductApplicationService
   Extending the service to support more products requires 2 changes, 1st inject new service and 2nd add next 'if' statement in SubmitApplicationFor method to handle/cover the new service.
   Readability of whole list of 'if' statements, as it expands, will get more and more difficult.
   Trivial refactoring here could be change this into switch statement, but still each case has some logic and the length of the method will be an issue.
   Solution is to use a strategy pattern.
2. ProductApplicationService has a single responsibility of implementing SubmitApplicationFor method. Presently the method owns logic related with each product, so there is a number or reasons for change. 
   Solution is as above, move the product-related logic into dedicated strategy.
3. Products folder has a plain structure without clear relation between products. Solution is to divide products into bounded contexts by grouping the files into subfolders/namespaces.
4. Interface IProduct is located in Product.cs file - the file shall be renamed to IProduct.cs to reflect the content and to help locate the code.
   The same for SellerApplication.cs and SellerCompanyData.cs that hold interfaces with classes.
5. Only one unit test with one assert of mocked service's result - this accually does not test anything except the moq library itself.  