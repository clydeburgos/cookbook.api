USE [Cookbook]
GO
/****** Object:  Table [dbo].[Recipe]    Script Date: 21/04/2019 3:01:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipe](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[Name] [nvarchar](150) NULL,
	[Ingredients] [nvarchar](max) NULL,
	[Instructions] [nvarchar](max) NULL,
	[PhotoUrl] [nvarchar](550) NULL,
 CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[Recipe] ([Id], [UserId], [Name], [Ingredients], [Instructions], [PhotoUrl]) VALUES (N'4eb847f9-94d1-4fa6-98b7-0d6ee319b193', N'pNjldhzu9kex7NAcZDinTGBMsRw1', N'Grilled Cheese', N'Bread,Butter,Cheese', N'Place bread butter-side-down onto skillet bottom and add 1 slice of cheese. Butter a second slice of bread on one side and place butter-side-up on top of sandwich. Grill until lightly browned and flip over; continue grilling until cheese is melted. Repeat with remaining 2 slices of bread, butter and ', N'https://www.seriouseats.com/recipes/images/2013/04/20130416-grilled-cheese-variations-2-10.jpg')
INSERT [dbo].[Recipe] ([Id], [UserId], [Name], [Ingredients], [Instructions], [PhotoUrl]) VALUES (N'76bc8c29-1cb3-4110-9d89-26cf4d6a3617', N'pNjldhzu9kex7NAcZDinTGBMsRw1', N'Pasta Salad', N'Chilled Pasta,Vinegar,Oil', N'Bring a large pot of lightly salted water to a boil. Add the macaroni, and cook until tender, about 8 minutes. Rinse under cold water and drain. In a large bowl, mix together the mayonnaise, vinegar, sugar, mustard, salt and pepper. Stir in the onion, celery, green pepper, carrot, pimentos and ', N'https://images-gmi-pmc.edge-generalmills.com/1f7b79bb-a401-4fa5-98bd-b15fcfd74bb4.jpg')
INSERT [dbo].[Recipe] ([Id], [UserId], [Name], [Ingredients], [Instructions], [PhotoUrl]) VALUES (N'f2901bd3-4f7b-4841-bf39-60c9295b75a9', N'2KABy8HgjnZWrUH02wzOiPQg44w1', N'Macarons', N'Eggs,Confectioners Sugar,Almond,Flour,Salt,Castor sugar', N'Gather ingredients. Preheat the oven to 300 F/140 C. If your oven happens to have a fan in it, we recommend not to use it if possible. Sieve the icing sugar and ground almonds into a large mixing bowl. In a separate, scrupulously clean bowl whisk the egg whites and salt until they form soft peaks', N'https://www.teatimemagazine.com/wp-content/uploads/2016/11/Vanilla-French-Macarons-696x537.jpg')
INSERT [dbo].[Recipe] ([Id], [UserId], [Name], [Ingredients], [Instructions], [PhotoUrl]) VALUES (N'4a1df88c-25d3-4350-8e81-61530407d008', N'AQtUGyJVwOMxiZOjoDCZO5vMfG23', N'Sweet and Sour Fish Fillet', N'Fish,Tomatoes,Garlic cloves,Onion', N'Cook ingredients and fry fish then mix. Mix it well when the fish has been fried well.', N'http://4.bp.blogspot.com/-DjlUL4dTBIA/UAZMggsKktI/AAAAAAAABgU/EMzrqDiD5yc/s1600/sweet-and-sour-fish-fillet.jpg')
INSERT [dbo].[Recipe] ([Id], [UserId], [Name], [Ingredients], [Instructions], [PhotoUrl]) VALUES (N'b57bfa12-8815-4031-a500-c3a37e4d36b3', N'glpJQ8miPAMZuQJt4xmDmd15hhJ3', N'Chicken Adobo', N'Chicken,Soy Sauce,Garlic Cloves,Onion', N'Marinate and Saute', N'https://becauseiamuniquelyandwonderfullymade.files.wordpress.com/2012/09/chicken-adobo.jpg')
INSERT [dbo].[Recipe] ([Id], [UserId], [Name], [Ingredients], [Instructions], [PhotoUrl]) VALUES (N'39f87023-6dd3-4ca6-acfc-d604af4f1871', N'glpJQ8miPAMZuQJt4xmDmd15hhJ3', N'Buffalo Chicken Wings', N'12 pieces chicken wings,1/2 teaspoon chili powder,1/2 teaspoon paprika,5 tablespoons butter,3 tablespoons hot sauce', N'Combine flour, chili powder, salt, and paprika in a mixing bowl. Mix well.
Put the chicken in the mixing bowl and coat with the flour mixture.
Heat a cooking pot and pour-in cooking oil.
Deep fry the chicken wings until golden brown. Set aside.
Melt the butter in a sauce pan.
Put-in hot sauce, ground black pepper, and garlic powder. Stir.
Pour-in the butter mixture over the fried chicken wings. Mix to distribute the sauce evenly.
Transfer on a serving plate.
Serve with your favorite dip. Share and enjoy!', N'https://images-gmi-pmc.edge-generalmills.com/b747b482-21b2-418d-9a34-5e6c1036072c.jpg')
INSERT [dbo].[Recipe] ([Id], [UserId], [Name], [Ingredients], [Instructions], [PhotoUrl]) VALUES (N'6b0aade2-fbe7-4fb1-97fe-dd00c31c13f9', N'2KABy8HgjnZWrUH02wzOiPQg44w1', N'Cake', N'Sugar,Butter,Butter,Butter,Vanilla Extract,Milk,Baking Powder', N'Gather your ingredients. Pound cake is one of the simplest cakes to bake. Preheat the oven to 325 °F (163 °C) and grease and flour a cake pan. Cream the butter and sugar. Add the eggs and vanilla. Stir in the cake flour. Pour the batter into the pan. Bake the cake for 1 hour 15 minutes.', N'https://images-gmi-pmc.edge-generalmills.com/b747b482-21b2-418d-9a34-5e6c1036072c.jpg')
INSERT [dbo].[Recipe] ([Id], [UserId], [Name], [Ingredients], [Instructions], [PhotoUrl]) VALUES (N'd7692521-1c02-466a-9fcd-e9170512f3e5', N'glpJQ8miPAMZuQJt4xmDmd15hhJ3', N'Teriyaki Chicken', N'1.3 pounds (600 grams) skinless boneless chicken thighs, (cut into 1 1/2-inch pieces),1 tablespoon cooking oil,1/4 cup low-sodium soy sauce,3 tablespoons light brown sugar ((or white granulated sugar))', N'Heat cooking oil in a large pan over medium heat. Stir fry chicken, stirring occasionally until lightly browned and crisp.

In a small jug or bowl whisk together the soy sauce, sugar, Sake/vinegar, Mirin and sesame oil to combine. Set aside.

Add the garlic to the centre of the pan and saute until lightly fragrant (about 30 seconds). Pour in the sauce and allow to cook, while stirring, until the sauce thickens into a beautiful shiny glaze (about 2-3 minutes).*', N'https://lewdfood.files.wordpress.com/2011/02/dsc_0035.jpg')
