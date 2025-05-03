def find_lines(grid):
    lines = []
    rows = len(grid)
    cols = len(grid[0])

    # Check horizontal lines
    for r in range(rows):
        count = 1
        for c in range(1, cols):
            if grid[r][c] == grid[r][c - 1] and grid[r][c] in ('b', 'c'):
                count += 1
            else:
                if count >= 2:
                    lines.append([(r, c - i) for i in range(count)])
                count = 1
        if count >= 2:
            lines.append([(r, cols - i - 1) for i in range(count)])

    # Check vertical lines
    for c in range(cols):
        count = 1
        for r in range(1, rows):
            if grid[r][c] == grid[r - 1][c] and grid[r][c] in ('b', 'c'):
                count += 1
            else:
                if count >= 2:
                    lines.append([(r - i, c) for i in range(count)])
                count = 1
        if count >= 2:
            lines.append([(rows - i - 1, c) for i in range(count)])

    return lines

# Step 3: Check Adjacency of 'd'

# Next, ensure that item 'd' is adjacent to item 'a' or 'b'. You can define adjacency as being in the same row or column, or diagonally adjacent.
# Example Code to Check Adjacency:

def is_adjacent(d_pos, a_pos):
    return (abs(d_pos[0] - a_pos[0]) <= 1 and abs(d_pos[1] - a_pos[1]) <= 1)

# Step 4: Path Traversal Algorithm

# You can use a depth-first search (DFS) or breadth-first search (BFS) algorithm to traverse the lines starting from the position of 'd'. The algorithm should mark lines as visited to avoid revisiting them.
# Example Code for Path Traversal:

def traverse_lines(grid, d_pos, lines):
    visited = set()
    stack = [d_pos]
    
    while stack:
        current = stack.pop()
        if current in visited:
            continue
        visited.add(current)

        # Check if current position is part of any line
        for line in lines:
            if current in line:
                # Mark the line as visited
                for pos in line:
                    visited.add(pos)
                # Add adjacent positions to stack
                for pos in line:
                    for adj in get_adjacent_positions(pos):
                        if adj not in visited:
                            stack.append(adj)

def get_adjacent_positions(pos):
    # Return all adjacent positions (up, down, left, right)
    return [(pos[0] + dx, pos[1] + dy) for dx, dy in [(-1, 0), (1, 0), (0, -1), (0, 1)]]

# Step 5: Putting It All Together

# Combine all the steps into a main function that initializes the grid, finds the lines, checks adjacency, and then traverses the lines.
# Example Main Function:


def main(grid):
    d_pos = None
    a_pos = None
    for r in range(len(grid)):
        for c in range(len(grid[0])):
            if grid[r][c] == 'd':
                d_pos = (r, c)
            elif grid[r][c] == 'a':
                a_pos = (r, c)

    if not is_adjacent(d_pos, a_pos):
        print("Item 'd' is not adjacent to item 'a'.")
        return

    lines = find_lines(grid)
    traverse_lines(grid, d_pos, lines)
    print("Traversal complete.")

# Example grid
grid = [
    ['a', 'a', 'b', 'a', 'a'],
    ['a', 'a', 'b', 'a', 'a'],
    ['a', 'a', 'c', 'b', 'd'],
    ['c', 'c', 'b', 'c', 'c'],
    ['a', 'a', 'c', 'a', 'a'],
    ['a', 'a', 'c', 'a', 'a']
]

if __name__ == '__main__':
    main(grid)




